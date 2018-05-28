using System.Dynamic;

public class Hal {
    public static dynamic Href(string url) {
        return new { href = url };
    }

    /// <summary>Generate a hypermedia links object containing first/final/prev/next links for paging through datasets.</summary>
    /// <param name="path">The absolute URL path to be decorated with paging querystring parameters</param>
    /// <param name="index">The index of the first record on the current page</param>
    /// <param name="count">The count of items on each page</param>
    /// <param name="total">The total number of items in the collection</param>
    /// <returns></returns>
    public static dynamic Paginate(string path, int index, int count, int total) {
        dynamic _links = new ExpandoObject();
        var maxIndex = total - 1;
        _links.first = Href($"{path}?index=0");
        _links.final = Href($"{path}?index={maxIndex - maxIndex % count}");
        if (index > 0) _links.previous = Href($"{path}?index={index - count}");
        if (index + count < maxIndex) _links.next = Href($"{path}?index={index + count}");
        return _links;
    }
}
