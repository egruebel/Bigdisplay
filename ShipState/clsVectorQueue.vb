Imports System.Threading
Imports System.Threading.Tasks

Public Class clsVectorQueue

    Private q As New Queue(Of clsVector)
    Private _MaxCount As Integer
    Private _FadeFactor As Double
    Private _Visible As Boolean

    Private _Origin As PointF
    Private _ScaleFactor As Single
    Private _PointsIn As Boolean
    Private _BarbWidthRatio As Single
    Private _BarbLengthRatio As Single
    Private _LineWidth As Single
    Private _LineColor As Color
    Private qLock As New Object 
    Private lastVector As clsVector

    Public Task As Task
    Public CancellationTokenSource As New CancellationTokenSource 
    Public CancellationToken As CancellationToken = CancellationTokenSource.Token

    Friend Sub New(ByVal PointsIn As Boolean)

        _MaxCount = 0
        _FadeFactor = 2
        _Visible = True
        _PointsIn = PointsIn

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
    Friend Sub add(ByVal dir As Single, ByVal mag As Single, ByVal TipRadius As Single)

        Dim v As New clsVector(_PointsIn)

        v.Direction = dir
        v.Magnitude = mag
        v.TipRadius = TipRadius

        v.BarbLengthRatio = _BarbLengthRatio
        v.BarbWidthRatio = _BarbWidthRatio

        v.ScaleFactor = _ScaleFactor
        v.Origin = _Origin
        v.LineWidth = _LineWidth
        v.Update()
        lastVector = v
        SyncLock qLock
            q.Enqueue(v)
            'q.Dequeue()
            If q.Count > _MaxCount Then
                q.Dequeue()
            End If
            'While q.Count > _MaxCount
                'Dim trash As clsVector = q.Dequeue()
                'trash.Dispose()
            'End While
        End SyncLock
        
        

    End Sub
   
    Friend Sub Dequeue()

        If q.Count > 0 Then
            Dim trash As clsVector = q.Dequeue()
            trash.dispose()
        End If

    End Sub

    Friend Function GetLast() As clsVector
        'If(q.Count > 0) Then
            'Return q.Peek()
        'End If
        'Return Nothing
        Return lastVector
    End Function

    Friend Sub Dispose()
        Debug.Print(LineColor.ToString)
        For Each v As clsVector In q
            v.Dispose()
        Next
        q.Clear()
        q = Nothing
    End Sub
    Friend Sub Draw(ByVal g As Graphics)

        If Not _Visible Then Exit Sub

        Dim i As Integer

        SyncLock qLock
            For Each v As clsVector In q
                i += 1
                v.LineColor = Color.FromArgb(CType(255 / Math.Pow(_FadeFactor, q.Count - i), Integer), _LineColor)
                v.Draw(g)
            Next
        End SyncLock
        

    End Sub
    Friend Sub Update()

        For Each v As clsVector In q
            v.ScaleFactor = _ScaleFactor
            v.Origin = _Origin
            v.LineWidth = _LineWidth
            v.Update()
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

    Friend Property ScaleFactor() As Single
        Get
            Return _ScaleFactor
        End Get
        Set(ByVal value As Single)
            _ScaleFactor = value
        End Set
    End Property
 
    Friend Property PointsIn() As Boolean
        Get
            Return _PointsIn
        End Get
        Set(ByVal value As Boolean)
            _PointsIn = value
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

    Friend Property BarbWidthRatio() As Single
        Get
            Return _BarbWidthRatio
        End Get
        Set(ByVal value As Single)
            _BarbWidthRatio = value
        End Set
    End Property
    Friend Property BarbLengthRatio() As Single
        Get
            Return _BarbLengthRatio
        End Get
        Set(ByVal value As Single)
            _BarbLengthRatio = value
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
