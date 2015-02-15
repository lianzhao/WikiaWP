using System;

namespace MediaWiki.Query.AllPages
{
    [Flags]
    public enum approp
    {
        none = 0,

        categories = 2 ^ 0,

        categoryinfo = 2 ^ 1,

        contributors = 2 ^ 2,

        coordinates = 2 ^ 3,

        deletedrevisions = 2 ^ 4,

        duplicatefiles = 2 ^ 5,

        extlinks = 2 ^ 6,

        extracts = 2 ^ 7,

        fileusage = 2 ^ 8,

        flagged = 2 ^ 9,

        flowinfo = 2 ^ 10,

        globalusage = 2 ^ 11,

        imageinfo = 2 ^ 12,

        images = 2 ^ 13,

        info = 2 ^ 14,

        iwlinks = 2 ^ 15,

        langlinks = 2 ^ 16,

        links = 2 ^ 17,

        linkshere = 2 ^ 18,

        pageimages = 2 ^ 19,

        pageprops = 2 ^ 20,

        pageterms = 2 ^ 21,

        redirects = 2 ^ 22,

        revisions = 2 ^ 23,

        stashimageinfo = 2 ^ 24,

        templates = 2 ^ 25,

        transcludedin = 2 ^ 26,

        transcodestatus = 2 ^ 27,

        videoinfo = 2 ^ 28
    }
}