using OtpNet;
using QRCoder;

namespace UnivMVC.Web.Helpers
{
    public class QR
    {
        
        public static string GenerateCodeUri(string usr, string? secreto)
        {
            string issuer = "UnivMVC";
            string uri = $"otpauth://totp/{issuer}:{usr}?secret={secreto}&issuer={issuer}&digits=6";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new SvgQRCode(qrCodeData);

            string svgImage = qrCode.GetGraphic(5);

            return "data:image/svg+xml;base64," + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svgImage));
        }
        

        /*
        public static string GenerateCodeUri(string usr, string secreto)
        {
            string issuer = "UnivMVC";
            
            if (!Guid.TryParse(secreto, out Guid secretoGuid))
            {
                throw new ArgumentException("El valor del secreto no es un GUID válido.");
            }                
            
            byte[] guidBytes = secretoGuid.ToByteArray();
            string base32Secret = Base32Encoding.ToString(guidBytes);
            
            string uri = $"otpauth://totp/{issuer}:{usr}?secret={base32Secret}&issuer={issuer}&digits=6";
            
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new SvgQRCode(qrCodeData);

            string svgImage = qrCode.GetGraphic(5);

            return "data:image/svg+xml;base64," + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svgImage));
        }
        */
    }
}
