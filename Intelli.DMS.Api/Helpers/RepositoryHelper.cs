using System;
using System.IO;

namespace Intelli.DMS.Api.Helpers
{
    public static class RepositoryHelper
    {
        public static string BuildRepositoryFilePath(string root,
                                                     int batchId,
                                                     int batchItemId,
                                                     string fileName,
                                                     DateTime createdDate,
                                                     int fileNo = 1)
        {
            var folder1 = createdDate.ToString("yyyy-MM");
            var folder2 = createdDate.ToString("yyyy-MM-dd");
            var folder3 = string.Format("{0}", batchId);
            var file = string.Format("{0}_{1}_{2:###}{3}", batchId, batchItemId, fileNo, Path.GetExtension(fileName));

            var path = Path.Combine(root, folder1, folder2, folder3, file);

            return path;
        }
        public static string BuildRepositoryFilePathForMultipleFiles(string root,
                                                  int batchId,
                                                  int batchItemId,
                                                  string fileName,
                                                  string fileType,
                                                  DateTime createdDate)
        {
            var folder1 = createdDate.ToString("yyyy-MM");
            var folder2 = createdDate.ToString("yyyy-MM-dd");
            var folder3 = string.Format("{0}", batchId);
            var file = string.Format("{0}_{1}_{2:###}{3}", batchId, batchItemId, fileName, "."+fileType);

            var path = Path.Combine(root, folder1, folder2, folder3, file);

            return path;
        }
        public static string BuildBatchItemReference(int batchId,
                                                     int batchItemId)
        {
            var file = string.Format("{0}_{1}", batchId, batchItemId);
            return file;
        }
        public static string BuildFileName(int batchId,
                                                     int batchItemId,
                                                     string fileName,
                                                     int fileNo = 1)
        {
            var file = string.Format("{0}_{1}_{2:###}{3}", batchId, batchItemId, fileNo, Path.GetExtension(fileName));
            return file;

        }

        public static string BuildTempFileName(int userId,
                                                            int tempFileId,
                                                            string fileName)
        {
            var file = string.Format("{0}_{1:###}{2}", userId, tempFileId, Path.GetExtension(fileName));
            return file;
        }

        public static string BuildRepositoryFilePathforTempFiles(string root,
                                                                 int userId,
                                                                 int tempFileId,
                                                                 string fileName)
        {
            var folder = string.Format("{0}", userId);
            var file = string.Format("{0}_{1:###}{2}", userId, tempFileId, Path.GetExtension(fileName));
            var path = Path.Combine(root, folder, file);
            return path;
        }
        public static string BuildUrlFilePath(string root,
                                                     int batchId,
                                                     string fileName,
                                                     DateTime createdDate)
        {
            var folder1 = createdDate.ToString("yyyy-MM");
            var folder2 = createdDate.ToString("yyyy-MM-dd");
            var folder3 = string.Format("{0}", batchId);

            var path = root + "/" + folder1 + "/" + folder2 + "/" + folder3 + "/" + fileName;
            return path;
        }

        public static string BuildFilePath(string root,
                                                     int batchId,
                                                     string fileName,
                                                     DateTime createdDate)
        {
            var folder1 = createdDate.ToString("yyyy-MM");
            var folder2 = createdDate.ToString("yyyy-MM-dd");
            var folder3 = string.Format("{0}", batchId);

            var path = Path.Combine(root, folder1, folder2, folder3, fileName);
            return path;
        }
    }
}
