using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NETWebFormsBlot
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Sanitize file name to prevent directory traversal
            string originalFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
            if (string.IsNullOrWhiteSpace(originalFileName) || originalFileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                // Invalid file name, handle error
                throw new InvalidOperationException("Invalid file name.");
            }

            // Optionally, restrict allowed file extensions
            string[] allowedExtensions = { ".txt", ".jpg", ".png", ".pdf" };
            string fileExtension = Path.GetExtension(originalFileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("File type not allowed.");
            }

            string safeDirectory = @"C:\uploaded_files\";
            string safePath = Path.Combine(safeDirectory, originalFileName);

            Stream stream = uploadFile.FileContent;
            if (stream != null)
            {
                // Save the uploaded file securely
                File.WriteAllBytes(safePath, uploadFile.FileBytes);

                // If you need to process the file content as text:
                byte[] buffer = ReadFully(stream);
                string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                // Optionally, write the converted content to a new file
                // File.WriteAllText(safePath, converted);
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}