using System;
using System.IO;

public class FileResourceManager : IDisposable
{
    private FileStream? _fileStream;
    private StreamWriter? _writer;
    private StreamReader? _reader;
    private bool _disposed = false;
    private readonly string _filePath;
    
    public FileResourceManager(string filePath)
    {
        _filePath = filePath;
    }
    
    public void OpenForWriting(FileMode mode = FileMode.OpenOrCreate)
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        
        _fileStream = new FileStream(_filePath, mode, FileAccess.Write);
        _writer = new StreamWriter(_fileStream);
    }
    
    public void OpenForReading()
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        
        if (!File.Exists(_filePath))
            throw new FileNotFoundException($"Файл не найден: {_filePath}");
        
        _fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        _reader = new StreamReader(_fileStream);
    }
    
    public void WriteLine(string text)
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        if (_writer == null) throw new InvalidOperationException("Сначала откройте файл для записи");
        
        _writer.WriteLine(text);
        _writer.Flush();
    }
    
    public string ReadAllText()
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        if (_reader == null) throw new InvalidOperationException("Откройте файл для чтения");
        
        return _reader.ReadToEnd();
    }
    
    public void AppendText(string text)
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        
        using (var writer = File.AppendText(_filePath))
        {
            writer.Write(text);
        }
    }
    
    public FileInfo GetFileInfo()
    {
        if (_disposed) throw new ObjectDisposedException("FileResourceManager");
        
        if (!File.Exists(_filePath))
            throw new FileNotFoundException($"Файл не найден: {_filePath}");
        
        return new FileInfo(_filePath);
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _writer?.Dispose();
            _reader?.Dispose();
            _fileStream?.Dispose();
            
            _disposed = true;
        }
        
        GC.SuppressFinalize(this);
    }
    
    ~FileResourceManager()
    {
        Dispose();
    }
}