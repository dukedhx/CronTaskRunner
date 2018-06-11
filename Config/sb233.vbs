Set fso = CreateObject("Scripting.FileSystemObject")
Set outputFile = fso.OpenTextFile( "text.txt", 8, True )
outputFile.WriteLine "233"

outputFile.Close