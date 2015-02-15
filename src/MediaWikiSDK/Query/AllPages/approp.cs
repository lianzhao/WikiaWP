using System;

namespace MediaWiki.Query.AllPages
{
    [Flags]
    public enum approp
    {
        none = 0x0,

        categories = 1,

        categoryinfo = 2,

        contributors = 4,

        coordinates = 8,

        deletedrevisions = 16,

        duplicatefiles = 32,

        extlinks = 64,

        extracts = 128,

        fileusage = 256,

        flagged = 512,

        flowinfo = 1024,

        globalusage = 2048,

        imageinfo = 4096,

        images = 8192,

        info = 16384,

        iwlinks = 32768,

        langlinks = 65536,

        links = 131072,

        linkshere = 262144,

        pageimages = 524288,

        pageprops = 1048576,

        pageterms = 2097152,

        redirects = 4194304,

        revisions = 8388608,

        stashimageinfo = 16777216,

        templates = 33554432,

        transcludedin = 67108864,

        transcodestatus = 134217728,

        videoinfo = 268435456
    }
}