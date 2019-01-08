Imports System.Threading
Imports System.Threading.Tasks

Public Class clsShipQueue

    Private q As New Queue(Of clsShip)
    Private _MaxCount As Integer
    Private _FadeFactor As Double
    Private _Visible As Boolean

    Private _Origin As PointF
    Private _Width As Single
    Private _Length As Single
    Private _LineWidth As Single
    Private _LineColor As Color
    Private qLock As New Object 
    Private lastShip As clsShip

    Public Task As Task
    Public CancellationTokenSource As New CancellationTokenSource 
    Public CancellationToken As CancellationToken = CancellationTokenSource.Token

    Friend Sub New()

        _MaxCount = 0
        _FadeFactor = 2
        _Visible = True

    End Sub
    Friend Property MaxCount() As Integer
        Get
            Return _MaxCount
        End Get
        Set(ByVal value As Integer)
            _MaxCount = value
        End Set
    End Property

    Friend Property FadeFactor() As Double
        Get
            Return _FadeFactor
        End Get
        Set(ByVal value As Double)
            If value >= 1 Then
                _FadeFactor = value
            Else
                _FadeFactor = 1
            End If
        End Set
    End Property

    Friend Sub Dequeue()

        If q.Count > 0 Then
            Dim trash As clsShip = q.Dequeue()
            trash.Dispose()
        End If

    End Sub

    Friend Sub Draw(ByVal g As Graphics)
        If Not _Visible Then Exit Sub

        Dim i As Integer

        SyncLock qLock
            For Each shp As clsShip In q
                i += 1
                shp.LineColor = Color.FromArgb(CType(255 / Math.Pow(_FadeFactor, q.Count - i), Integer), _LineColor)
                shp.Draw(g)
            Next
        End SyncLock
    End Sub
    Friend Sub add(ByVal head As Single)

        Dim shp As New clsShip()

        shp.Heading = head
        shp.Length = _Length
        shp.Width = _Width
        shp.Origin = _Origin
        shp.LineWidth = _LineWidth
        shp.Update()
        lastShip = shp

        SyncLock qLock
            q.Enqueue(shp)
            if q.Count > _MaxCount
                q.Dequeue()
            End if
        End SyncLock
    End Sub
    Friend Function GetLast() As clsShip
        Return lastShip
    End Function
    Friend Sub Update()

        For Each shp As clsShip In q
            shp.Length = _Length
            shp.Width = _Width
            shp.Origin = _Origin
            shp.LineWidth = _LineWidth
            shp.Update()
        Next

    End Sub
    Friend Property Origin() As PointF
        Get
            Return _Origin
        End Get
        Set(ByVal value As PointF)
            _Origin = value
        End Set
    End Property

    Friend Property Width() As Single
        Get
            Return _Width
        End Get
        Set(ByVal value As Single)
            _Width = value
        End Set
    End Property
    Friend Property Length() As Single
        Get
            Return _Length
        End Get
        Set(ByVal value As Single)
            _Length = value
        End Set
    End Property
    Friend Property LineWidth() As Single
        Get
            Return _LineWidth
        End Get
        Set(ByVal value As Single)
            _LineWidth = value
        End Set
    End Property
    Friend Property LineColor() As Color
        Get
            Return _LineColor
        End Get
        Set(ByVal value As Color)
            _LineColor = value
        End Set
    End Property
    Friend Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
        End Set
    End Property
End Class
