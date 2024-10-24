using App.Utils;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Data;


public enum DataFrom
{
    File,
    String,
    Url
}


public class DataSource
{
    private DataFrom _dataFrom;
    private string _content;

    public DataSource(DataFrom dataFrom, string content)
    {
        _content = content;
        _dataFrom = dataFrom;
    }

    public string GetContent() { return _content; }
    public DataFrom GetDataFrom() { return _dataFrom; }

    public static DataSource Store(string content)
    {
        return new DataSource(DataFrom.String, content);
    }

    public static DataSource FromFile(string path)
    {
        string file = FilesUtils.ReadFile(path);
        return new DataSource(DataFrom.File, file);
    }
}