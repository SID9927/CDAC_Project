using System;
using System.Collections.Generic;
using System.IO;
using DotnetBackend.Models;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DotnetBackend.Services
{
    public class PdfExportService
    {
        public void Export(List<CartItem> items)
        {
            using (var stream = new FileStream("receipt.pdf", FileMode.Create))
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var logo = ImageDataFactory.Create("fm.jpg");
                var image = new Image(logo).ScaleAbsolute(50, 50);
                document.Add(image);

                string fontFamily = "Arial";
                float fontSize = 12f; // Font size in points

                // Create the font
                PdfFont font = PdfFontFactory.CreateFont(fontFamily, PdfEncodings.WINANSI);
                Paragraph header = new Paragraph().AddTabStops().Add("Farmers Marketplace");

                // Set the font for the paragraph
                
                document.Add(header);

                document.Add(new LineSeparator(new SolidLine()));

                var table = new Table(4);
                AddTableHeader(table);
                AddRows(table, items);

                document.Add(table);

                double? grandTotal = 0.0;

                foreach (var item in items)
                {
                    grandTotal += item.Amount;
                }

                for (int i = 0; i < 10; i++)
                {
                    document.Add(new Paragraph("\n"));
                }

                document.Add(new Paragraph($"Total Amount: {grandTotal}").SetFont(font));

                document.Close();
            }
        }

        private void AddTableHeader(Table table)
        {
            foreach (var columnTitle in new[] { "Product Name", "Quantity", "Price", "Amount" })
            {
                var header = new Cell().SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                                        .SetBorder(new SolidBorder(2))
                                        .Add(new Paragraph(columnTitle));
                table.AddHeaderCell(header);
            }
        }

        private void AddRows(Table table, List<CartItem> items)
        {
            foreach (var cartItem in items)
            {
                table.AddCell(cartItem.Item);
                table.AddCell(cartItem.Qty.ToString());
                table.AddCell(cartItem.Price.ToString());
                table.AddCell(cartItem.Amount.ToString());
            }
        }
    }
}
