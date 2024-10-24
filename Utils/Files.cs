using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using DocumentFormat.OpenXml.Packaging;

namespace App.Utils;


public class Files
{
    public static string ReadFile(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();

        switch (extension)
        {
            case ".pdf":
                return Files.ReadPdfFile(filePath);
            case ".docx":
                return Files.ReadDocxFile(filePath);
            case ".txt":
                return Files.ReadTxtFile(filePath);
            default:
                throw new NotSupportedException($"File extension {extension} is not supported.");
        }
    }

    private static string ReadPdfFile(string filePath)
    {
        using (PdfReader reader = new PdfReader(filePath))
        using (PdfDocument pdf = new PdfDocument(reader))
        {
            StringBuilder text = new StringBuilder();
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }
            return text.ToString();
        }
    }

    private static string ReadDocxFile(string filePath)
    {
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
        {
            if (wordDoc.MainDocumentPart == null) return "";
            if (wordDoc.MainDocumentPart.Document.Body == null) return "";
            return wordDoc.MainDocumentPart.Document.Body.InnerText;
        }
    }

    private static string ReadTxtFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }

}