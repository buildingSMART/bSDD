// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Net.Mime;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace bSDD.DemoClientConsole.ApiHelper
// {
//     public class Uploader
//     {
//         public async Task UploadFile(string fileName, byte[] byteArray, CancellationToken cancellationToken)
//         {
//             // LogInstance.Logger.Trace($"Start sending file '{fileName}' to woodpulp ...");
//
//             var contentType = GetContentType(fileName);
//
//             var httpContent = new ByteArrayContent(byteArray);
//             httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
//             // using var formData = new MultipartFormDataContent { { httpContent, "uploadRequest.File", fileName } };
//             //
//             // await PostAsync(RelativeUri, formData, cancellationToken);
//         }
//
//         private static string GetContentType(string fileName)
//         {
//             var extension = Path.GetExtension(fileName);
//             string contentType;
//             switch (extension)
//             {
//                 case ".xml":
//                     contentType = MediaTypeNames.Application.Xml;
//                     break;
//                 case ".json":
//                     contentType = MediaTypeNames.Application.Json;
//                     break;
//                 default:
//                     contentType = MediaTypeNames.Application.Octet;
//                     break;
//             }
//
//             return contentType;
//         }
//     }
// }
