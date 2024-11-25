using iTextSharp.text.pdf;
using iTextSharp.text;
using PixApplication.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PixApplication.Business
{
    public class CobrancaPixBO
    {
        public static void GerarPDF(string cobrancapix, string chave, string status, string valor, string pixCopiaECola, string location)
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                string pdfFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cobranca_Pix_" + cobrancapix + ".pdf");

                PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));

                doc.Open();

                doc.Add(new Paragraph("Cobrança PIX"));
                doc.Add(new Paragraph("-------------------------------------------------"));

                doc.Add(new Paragraph($"ID da cobrança: {cobrancapix}"));
                doc.Add(new Paragraph($"Status: {status}"));
                doc.Add(new Paragraph($"Valor: {valor}"));
                doc.Add(new Paragraph($"Chave Pix: {chave}"));
                doc.Add(new Paragraph($"Solicitação do pagador: Serviço realizado"));
                doc.Add(new Paragraph($"Link de pagamento: {location}"));

                doc.Add(new Paragraph("QR Code:"));

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(pixCopiaECola, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        img.ScaleToFit(100f, 100f);
                        img.Alignment = Element.ALIGN_CENTER;
                        doc.Add(img);
                    }
                }

                doc.Close();
                MessageBox.Show($"PDF gerado com sucesso: {pdfFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF: {ex.Message}");
            }
        }
    }
}
