# Trifling.Common

## BoxedByteArrayComparer

A comparer for comparing the content of two byte arrays.

### Compare(x, y)

Compares two objects as byte arrays and returns their relative position.

| Name | Description |
| ---- | ----------- |
| x | *System.Object*<br>The first byte array to compare. |
| y | *System.Object*<br>The second byte array to compare. |

#### Returns

Returns -1 if x is before y. Returns 0 if they are equal. Otherwise returns 1.

### Default

Gets the default instance of the <a href="#boxedbytearraycomparer">BoxedByteArrayComparer</a>.


## ByteArrayComparer

A comparer for comparing the content of two byte arrays.

### Compare(a, b)

Compares two objects as byte arrays and returns their relative position.

| Name | Description |
| ---- | ----------- |
| a | *System.Byte[]*<br>The first byte array to compare. |
| b | *System.Byte[]*<br>The second byte array to compare. |

#### Returns

Returns -1 if a is before b. Returns 0 if they are equal. Otherwise returns 1.

### Default

Gets the default instance of the <a href="#bytearraycomparer">ByteArrayComparer</a>.


## CompressorConfiguration

The configuration options for the compressor implementation.

### Constructor

Initialises a new instance of the <a href="#compressorconfiguration">CompressorConfiguration</a> class with the default options.

#### Remarks

Default <a href="#compressorconfiguration.minimumsizetocompress">CompressorConfiguration.MinimumSizeToCompress</a> is 200 and default <a href="#compressorconfiguration.compressionlevel">CompressorConfiguration.CompressionLevel</a> is Fastest.

### Constructor(minimumSizeToCompress, compressionLevel)

Initialises a new instance of the <a href="#compressorconfiguration">CompressorConfiguration</a> class with the given property values.

| Name | Description |
| ---- | ----------- |
| minimumSizeToCompress | *System.Int32*<br>The minimum input size which will be considered valid for compression. Any value less than this value will not be compressed but will be returned unchanged. |
| compressionLevel | *System.IO.Compression.CompressionLevel*<br>The level of compression that the implementation will use when performing the compression. |

### CompressionLevel

Gets or sets the level of compression that the implementation will use when performing the compression.

### Equals(obj)

Determines, based on property values, whether or not this instance equals the given instance.

| Name | Description |
| ---- | ----------- |
| obj | *System.Object*<br>The instance to which the current instance is being compared. |

#### Returns

Returns true if both have the same values, otherwise false.

### Equals(other)

Determines, based on property values, whether or not this instance equals the given instance.

| Name | Description |
| ---- | ----------- |
| other | *Trifling.Compression.CompressorConfiguration*<br>The instance to which the current instance is being compared. |

#### Returns

Returns true if both have the same values, otherwise false.

### GetHashCode

Calculates the hash code for this instance based on the values of the properties.

#### Returns

Returns an integer hash code.

### MinimumSizeToCompress

Gets or sets the minimum input size which will be considered valid for compression. Any value less than this value will not be compressed but will be returned unchanged.

## LoggingTextWriter

Provides a <a href="#system.io.textwriter">System.IO.TextWriter</a> to write text to a <a href="#microsoft.extensions.logging.ilogger">Microsoft.Extensions.Logging.ILogger</a>.

### Constructor(targetLogger, outputLogLevel)

Initialises a new instance of the <a href="#loggingtextwriter">LoggingTextWriter</a> class with the given instance of a target logger to receive written messages.

| Name | Description |
| ---- | ----------- |
| targetLogger | *Microsoft.Extensions.Logging.ILogger*<br>An instance of an <a href="#microsoft.extensions.logging.ilogger">Microsoft.Extensions.Logging.ILogger</a> to which log entries are written. |
| outputLogLevel | *Microsoft.Extensions.Logging.LogLevel*<br>The log level at which all output log entries will be written. Default: Trace. |

### buffer

A short-term buffer of characters not yet written to the target logger.

### ClearBuffer

Clears the current buffer of characters waiting to be logged.

### Dispose(disposing)

Flushes any buffered values to the target logger before disposing of this object.

| Name | Description |
| ---- | ----------- |
| disposing | *System.Boolean*<br>Indicator of whether this is final disposal. |

### Encoding

Gets the encoding used for this text writer (Unicode).

### Flush

Flushes the current buffer contents to the target logger.

### FlushAsync

Asynchronously flushes the current buffer contents to the target logger.

#### Returns

Returns a <a href="#system.threading.tasks.task">System.Threading.Tasks.Task</a> for the flush operation.

### FormatProvider

Gets the format provider used for string formatting (Invariant).

### previousWasNewline

Indicates if the previous character received was a newline character. Multiple newline characters are ignored.

### Write(value)

Writes a character to the buffer before it is written out to the target logger. The buffer is written whenever newline characters are encountered or when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| value | *System.Char*<br>The character value to write to the logger. |

### Write(format, arg0, arg1, arg2)

Writes a formatted value to the buffer before it is written out to the target logger. The buffer is written when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>The first argument to substitute in the format string. |
| arg1 | *System.Object*<br>The second argument to substitute in the format string. |
| arg2 | *System.Object*<br>The third argument to substitute in the format string. |

### Write(format, arg0, arg1)

Writes a formatted value to the buffer before it is written out to the target logger. The buffer is written when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>The first argument to substitute in the format string. |
| arg1 | *System.Object*<br>The second argument to substitute in the format string. |

### Write(format, arg0)

Writes a formatted value to the buffer before it is written out to the target logger. The buffer is written when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>An argument to substitute in the format string. |

### Write(format, arg)

Writes a formatted value to the buffer before it is written out to the target logger. The buffer is written when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg | *System.Object[]*<br>All of the arguments to substitute in the format string. |

### Write(value)

Writes a string value to the buffer before it is written out to the target logger. The buffer is written when on of the many <a href="#loggingtextwriter.writeline">LoggingTextWriter.WriteLine</a> methods are called.

| Name | Description |
| ---- | ----------- |
| value | *System.String*<br>The unformatted string value to write to the log. |

### WriteBuffer

Writes the current content of the buffer to the target logger with the logging level defined in <a href="#loggingtextwriter._outputloglevel">LoggingTextWriter._outputLogLevel</a>.

### WriteLine

Writes the current contents of the buffer to the target logger. If the buffer is empty then no log entry is written to the target logger.

### WriteLine(format, arg0, arg1, arg2)

Writes the current contents of the buffer (with the given formatted string appended) to the target logger. If the string is empty then nothing is written to the target logger.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>The first argument to substitute in the format string. |
| arg1 | *System.Object*<br>The second argument to substitute in the format string. |
| arg2 | *System.Object*<br>The third argument to substitute in the format string. |

### WriteLine(format, arg0, arg1)

Writes the current contents of the buffer (with the given formatted string appended) to the target logger. If the string is empty then nothing is written to the target logger.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>The first argument to substitute in the format string. |
| arg1 | *System.Object*<br>The second argument to substitute in the format string. |

### WriteLine(format, arg0)

Writes the current contents of the buffer (with the given formatted string appended) to the target logger. If the string is empty then nothing is written to the target logger.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg0 | *System.Object*<br>An argument to substitute in the format string. |

### WriteLine(format, arg)

Writes the current contents of the buffer (with the given formatted string appended) to the target logger. If the string is empty then nothing is written to the target logger.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string to write. |
| arg | *System.Object[]*<br>All arguments to substitute in the format string. |

### WriteLine(value)

Writes the current contents of the buffer (with the given unformatted string appended) to the target logger. If the string is empty then nothing is written to the target logger.

| Name | Description |
| ---- | ----------- |
| value | *System.String*<br>The unformatted string value to write to the logger. |

### WriteString(format, args)

Writes a full formatted string to the target logger with the logging level defined in <a href="#loggingtextwriter._outputloglevel">LoggingTextWriter._outputLogLevel</a>.

| Name | Description |
| ---- | ----------- |
| format | *System.String*<br>The format string which defines where the args will appear in the string. |
| args | *System.Object[]*<br>The argument values used when constructing a format string. |

### WriteString(value)

Writes an unformatted string to the target logger with the logging level defined in <a href="#loggingtextwriter._outputloglevel">LoggingTextWriter._outputLogLevel</a>.

| Name | Description |
| ---- | ----------- |
| value | *System.String*<br>The unformatted string value to write to the logger. |

