using iTextSharp.text;
using iTextSharp.text.pdf;

try
{
    List<string> documentos = new List<string>
    {
        Environment.CurrentDirectory + @"/PDF1.pdf",
        Environment.CurrentDirectory + @"/PDF2.pdf",
        Environment.CurrentDirectory + @"/PDF3.pdf"
    };

    Document document = new Document();
    string archivoCompleto = "";

    archivoCompleto = Environment.CurrentDirectory + @"/Unificado" + ".pdf";

    using (FileStream newFileStream = new FileStream(archivoCompleto, FileMode.Create))
    {
        PdfCopy writer = new PdfCopy(document, newFileStream);
        document.Open();
        foreach (string fileName in documentos)
        {
            PdfReader reader = new PdfReader(fileName);
            reader.ConsolidateNamedDestinations();

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                writer.AddPage(page);
            }

            PRAcroForm form = reader.AcroForm;
            if (form != null)
            {
                writer.AddDocument(reader);
            }
            reader.Close();
        }
        writer.Close();
        document.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}