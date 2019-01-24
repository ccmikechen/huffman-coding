Imports System.IO
Public Class Form1
    
    Dim num(52) As Integer '節點的值 = 出現次數
    Dim tree(52) As String '樹
    Dim tn(52) As String '節點0與1
    Dim code(52) As String '編碼表
    Dim num2(52) As Integer '顯示用值
    Sub cn(ByVal str As String) '頻率
        For Each i In str
            If Asc(i) >= 97 And Asc(i) <= 122 Then
                num(Asc(i) - 96) += 1
                num2(Asc(i) - 96) += 1
            ElseIf Asc(i) >= 65 And Asc(i) <= 90 Then
                num(Asc(i) - 38) += 1
                num2(Asc(i) - 38) += 1
            End If
        Next
    End Sub

    Sub bt() '建樹
        Dim sum As Integer
        For i = 1 To 52
            sum += num(i)
        Next
        tree(0) = sum

        Do While True
            Dim n1, n2, min, t1, t2 As Integer
            min = sum
            For i = 0 To UBound(num)
                If num(i) < min Then min = num(i) : t1 = i
            Next
            n1 = min : num(t1) = sum
            min = sum
            For i = 0 To UBound(num)
                If num(i) < min Then min = num(i) : t2 = i
            Next
            n2 = min : num(t2) = sum
            min = sum
            '求2個最小值

            num(t1) = sum
            num(t2) = sum '捨棄用過的值
            tn(t1) = 0
            tn(t2) = 1 '節點的0或1

            Dim s As Integer = n1 + n2
            If s = sum Then
                tree(t1) = 0
                tree(t2) = 0
                Exit Do
            Else
                tree(t1) = UBound(tree) + 1
                tree(t2) = UBound(tree) + 1
                '設定父節點
                ReDim Preserve tree(UBound(tree) + 1)
                ReDim Preserve num(UBound(num) + 1)
                ReDim Preserve tn(UBound(tn) + 1)
                num(UBound(num)) = s
                '增加新節點 值=n1+n2
            End If

        Loop
    End Sub

    Sub sc() '設定編碼表
        For i = 1 To 52
            Dim c As String = ""
            Dim n As Integer = i
            Do Until n = 0
                c = tn(n) & c
                n = tree(n)
            Loop
            code(i) = c
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ReDim num(52) : ReDim tree(52) : ReDim tn(52) : ReDim code(52) : ReDim num2(52)
        Dim sr As New StreamReader("in.txt")
        Dim str As String = sr.ReadToEnd
        cn(str) : bt() : sc()
        TextBox1.Clear()
        For i = 1 To 26
            TextBox1.Text &= Chr(i + 96) & vbTab & num2(i) & vbTab & code(i) & vbCrLf
        Next
        For i = 27 To 52
            TextBox1.Text &= Chr(i + 38) & vbTab & num2(i) & vbTab & code(i) & vbCrLf
        Next
        sr.Close()
    End Sub

    Sub wtoc(ByVal str As String)
        Dim sw As New StreamWriter("wtoc.txt")
        Dim c As String
        For Each i In str
            If Asc(i) >= 97 And Asc(i) <= 122 Then
                c &= code(Asc(i) - 96)
            ElseIf Asc(i) >= 65 And Asc(i) <= 90 Then
                c &= code(Asc(i) - 38)
            Else
                c &= i
            End If
        Next
        TextBox1.Clear()
        TextBox1.Text = c
        sw.WriteLine(c)
        sw.Flush() : sw.Close()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sr As New StreamReader("in.txt")
        Dim str As String = sr.ReadToEnd
        wtoc(str)
        sr.Close()
    End Sub

    Sub ctow()
        Dim sw As New StreamWriter("ctow.txt")
        Dim sr As New StreamReader("wtoc.txt")
        Dim c, st As String
        Do While sr.Peek > -1
            c = ""
            Dim str As String = sr.ReadLine
            For Each i In str
                st &= i
                If Asc(st) = 48 Or Asc(st) = 49 Then
                    For j = 1 To 52
                        If st = code(j) Then
                            If j >= 1 And j <= 26 Then
                                c &= Chr(j + 96) : st = "" : Exit For
                            Else
                                c &= Chr(j + 38) : st = "" : Exit For
                            End If
                        End If
                    Next
                Else
                    c &= st : st = ""
                End If
            Next
            TextBox1.Text &= c & vbCrLf
            sw.WriteLine(c)
        Loop
        sw.Flush() : sw.Close() : sr.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Clear()
        ctow()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sr As New StreamReader("in.txt")
        TextBox1.Text = sr.ReadToEnd
        sr.Close()
    End Sub

End Class
