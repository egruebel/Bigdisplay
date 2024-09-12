using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpSimulator
{
    class Program
    {
        private static UdpClient _channelgga = new UdpClient();
        private static UdpClient _channelhdg = new UdpClient();
        private static UdpClient _channelwind = new UdpClient();
        private static UdpClient _channelatt = new UdpClient();

        private static UdpClient _channelMet = new UdpClient();
        private static UdpClient _channelBowMet = new UdpClient();

        private static UdpClient _channelSpeedLog = new UdpClient();


        static void Main(string[] args)
        {
            Random r = new Random();
            Single roll = 0;
            bool rollDir = true;
            Single limit = 20;
            bool pitchDir = true;
            Single pitch = 0;
            _channelgga.Connect(new IPEndPoint(IPAddress.Broadcast, 16002));
            _channelhdg.Connect(new IPEndPoint(IPAddress.Broadcast, 16004));
            _channelwind.Connect(new IPEndPoint(IPAddress.Broadcast, 16007));
            _channelatt.Connect(new IPEndPoint(IPAddress.Broadcast, 16005));
            _channelMet.Connect(new IPEndPoint(IPAddress.Broadcast, 16006));
            _channelBowMet.Connect(new IPEndPoint(IPAddress.Broadcast, 16011));
            _channelSpeedLog.Connect(new IPEndPoint(IPAddress.Broadcast, 16014));

            while (true)
            {
                var str1 = $"$GPVTG,{Compass()},2,3,4,{Speed()},6,7,8*{((byte)r.Next(0, 255)).ToString("X2")}\r\n"; //1,5
                var str2 = $"$HEHDT,{Compass()},2,3,4,5*{((byte)r.Next(100, 255)).ToString("X2")}\r\n";
                var str3 = $"$PTRUEWIND,1,{Speed()},{Compass()},{Speed()},{Compass()},6,7,{Speed()},{Compass()},{Speed()},{Compass()},12*{((byte)r.Next(100, 255)).ToString("X2")}\r\n";
                var str4 = $"$GPGGA,1,{Lat()},{Lon()},{r.Next(1,3)},7,8,9,10,11,12,13*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                var str5 = $"$GPPAT,1,{NextRoll()},{NextPitch()},{r.Next(0, 4)},7,8,9,10,11,12,13*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                var str6 = $"$PMET,{r.Next(20,60)},2,{r.Next(0,100)},4,{r.Next(0,100)},6,7,8,{r.Next(20,60)},10,{r.Next(0,10)},12,{r.Next(0,10)},14,15*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                var str7 = $"$WIXDR,1,{r.Next(20,60)},3,4,5,{r.Next(0,100)},7,8,9,{r.Next(0,100)},11,12,13,14*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                var str8 = $"$VBW,{r.Next(4, 10)},{r.Next(1, 3)},{1},4,5,6*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                _channelatt.Send(Encoding.ASCII.GetBytes(str5), str5.Length);
                _channelgga.Send(Encoding.ASCII.GetBytes(str1), str1.Length);
                Thread.Sleep(33);
                _channelhdg.Send(Encoding.ASCII.GetBytes(str2), str2.Length);
                Thread.Sleep(33);
                _channelSpeedLog.Send(Encoding.ASCII.GetBytes(str8), str8.Length);
                _channelwind.Send(Encoding.ASCII.GetBytes(str3), str3.Length);
                Thread.Sleep(490);
                str5 = $"$GPPAT,1,{NextRoll()},{NextPitch()},{r.Next(0, 4)},7,8,9,10,11,12,13*{((byte)r.Next(0, 255)).ToString("X2")}\r\n";
                _channelatt.Send(Encoding.ASCII.GetBytes(str5), str5.Length);
                _channelgga.Send(Encoding.ASCII.GetBytes(str4), str4.Length);
                _channelMet.Send(Encoding.ASCII.GetBytes(str6), str6.Length);
                _channelBowMet.Send(Encoding.ASCII.GetBytes(str7), str7.Length);
                Thread.Sleep(444);
                Console.WriteLine(_channelgga.Client.LocalEndPoint.ToString() + ": " + str1);
                Console.WriteLine(_channelhdg.Client.LocalEndPoint.ToString() + ": " + str2);
                Console.WriteLine(_channelwind.Client.LocalEndPoint.ToString() + ": " + str3);
                Console.WriteLine(_channelgga.Client.LocalEndPoint.ToString() + ": " + str4);
                Console.WriteLine(_channelMet.Client.LocalEndPoint.ToString() + ": " + str6);
                Console.WriteLine(_channelBowMet.Client.LocalEndPoint.ToString() + ": " + str7);
                Console.WriteLine(_channelSpeedLog.Client.LocalEndPoint.ToString() + ": " + str8);

            }
            String Compass() //returns a heading between 0 and 359
            {
                return (r.Next(350, 360) ).ToString("000.00");
            }

            String Speed() //returns a speed between 4 and 10 knots
            {
                return (r.Next(4, 10)).ToString();
            }

            String Lat()
            {
                string deg = r.Next(0, 89).ToString("00");
                string min = (r.Next(0, 59) * r.NextDouble()).ToString("00.00000");
                string ns = r.NextDouble() > .5 ? "N" : "S";
                return $"{deg}{min},{ns}";
            }

            String Lon()
            {
                string deg = r.Next(0, 180).ToString("000");
                string min = (r.Next(0, 59) * r.NextDouble()).ToString("00.00000");
                string ew = r.NextDouble() > .5 ? "E" : "W";
                return $"{deg}{min},{ew}";
            }

            Single NextRoll()
            {
                if (rollDir)
                {
                    if (roll >= limit)
                        rollDir = false;
                    roll += (Single)( r.NextDouble() * 4.1);
                }
                else
                {
                    if (roll <= limit * -1)
                        rollDir = true;
                    roll -= (Single)(r.NextDouble() * 4.1);
                }

                return roll;
            }

            Single NextPitch()
            {
                var pitchlimit = 12;
                if (pitchDir)
                {
                    if (pitch >= pitchlimit)
                    {
                        pitchDir = false;
                        
                    }
                    else
                    {
                        pitch += (Single)(r.NextDouble() * 4.1);
                        if (pitch > pitchlimit)
                            pitch = pitchlimit;
                    }
                }
                else
                {
                    if (pitch <= pitchlimit * -1)
                    {
                        pitchDir = true;
                        
                    }
                    else
                    {
                        pitch -= (Single)(r.NextDouble() * 4.1);
                        if (pitch < pitchlimit * -1)
                            pitch = pitchlimit * -1;
                    }   
                    
                }
                return pitch;
            }
        }

        
    }
}
