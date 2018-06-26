Set fso = CreateObject("Scripting.FileSystemObject")
Set outputFile = fso.OpenTextFile( "C:\\Users\\bhuang\\source\\repos\\WebApplication1\\WebApplication2\\Config\\text.txt", 8, True )
outputFile.WriteLine "233"

outputFile.Close