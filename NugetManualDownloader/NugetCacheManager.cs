﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetManualDownloader
{
    class NugetCacheManager
    {
        public string CacheFolderPath { get; }
        public NuGet.Packaging.PackageSaveMode PackageSaveMode { get; }
        public NugetCacheManager(string cachePath, NuGet.Packaging.PackageSaveMode packageSaveMode = NuGet.Packaging.PackageSaveMode.Defaultv3)
        {
            CacheFolderPath = cachePath;
            PackageSaveMode = packageSaveMode;
        }
        public void Put(Stream nupkgFile)
        {
            byte[] sha512value;
            Console.WriteLine("Computing Hash...");
            using (var sha512 = System.Security.Cryptography.SHA512.Create())
            {
                sha512value = sha512.ComputeHash(nupkgFile);
            }
            Console.WriteLine("Writing...");
            nupkgFile.Seek(0, SeekOrigin.Begin);
            NuGet.Packaging.PackageArchiveReader packageArchiveReader = new NuGet.Packaging.PackageArchiveReader(nupkgFile);
            var pkid = packageArchiveReader.GetIdentity();
            Directory.CreateDirectory(Path.Combine(CacheFolderPath, pkid.Id));
            var pkfilderstr = Path.Combine(CacheFolderPath, pkid.Id, pkid.Version.ToNormalizedString());

            Directory.CreateDirectory(pkfilderstr);
            foreach(var one in NuGet.Packaging.PackageReaderExtensions.GetPackageFiles(packageArchiveReader, PackageSaveMode))
            {
                packageArchiveReader.ExtractFile(one, Path.Combine(pkfilderstr, one), new MyLogger());
            }
        }
    }
}