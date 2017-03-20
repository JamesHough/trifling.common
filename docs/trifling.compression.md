# Trifling.Compression

## ICompressor

A compressor which can compress and decompress data.

### Compress(source)

Compresses a source byte array and returns a compressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to compress. |

#### Returns

Returns a byte array containing the compressed data.

### CompressStream(inputStream, outputStream)

Compresses the input stream and writes the compressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be compressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the compressed data must be written. |

### Decompress(source)

Decompresses the given source byte array and returns a decompressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to decompress. |

#### Returns

Returns a byte array containing the decompressed data.

### DecompressStream(inputStream, outputStream)

Decompresses the input stream and writes the decompressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be decompressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the decompressed data must be written. |


## ICompressorFactory

A factory for generating implementations of compressor interfaces.

### Create\<T\>(configuration)

Creates a new instance of the compressor which matches the requested type T.

#### Type Parameters

- T - The type of compressor to create.

| Name | Description |
| ---- | ----------- |
| configuration | *Trifling.Compression.CompressorConfiguration*<br>The configuration to use when generating the instance of the compressor. |

#### Returns

Returns an implementation of the compressor requested.


## IDeflateCompressor

An implementation of a Deflate compression engine.


## IGzipCompressor

An implementation of a G-Zip compression engine.


## CachedCompressorFactory

A factory for creating instances of compressors.

### Create\<T\>(configuration)

Creates a new instance of the compressor which matches the requested type T.

#### Type Parameters

- T - The type of compressor to create.

| Name | Description |
| ---- | ----------- |
| configuration | *Trifling.Compression.CompressorConfiguration*<br>The configuration to use when generating the instance of the compressor. |

#### Returns

Returns an implementation of the compressor requested.


## CompressorFactory

A factory for creating instances of compressors.

### Create\<T\>(configuration)

Creates a new instance of the compressor which matches the requested type T.

#### Type Parameters

- T - The type of compressor to create.

| Name | Description |
| ---- | ----------- |
| configuration | *Trifling.Compression.CompressorConfiguration*<br>The configuration to use when generating the instance of the compressor. |

#### Returns

Returns an implementation of the compressor requested.


## DeflateCompressor

A compressor which can compress and decompress data in Deflate format.

### Constructor

Initialises a new instance of the <a href="#deflatecompressor">DeflateCompressor</a> class with the default configuration.

### Constructor(configuration)

Initialises a new instance of the <a href="#deflatecompressor">DeflateCompressor</a> class with the specified configuration.

| Name | Description |
| ---- | ----------- |
| configuration | *Microsoft.Extensions.Options.IOptions{Trifling.Compression.CompressorConfiguration}*<br>The configuration options for the compression engine. |

### Constructor(configuration)

Initialises a new instance of the <a href="#deflatecompressor">DeflateCompressor</a> class with the specified configuration.

| Name | Description |
| ---- | ----------- |
| configuration | *Trifling.Compression.CompressorConfiguration*<br>The configuration options for the compression engine. |

### Compress(source)

Compresses a source byte array and returns a Deflate compressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to compress. |

#### Returns

Returns a byte array containing the compressed data.

#### Remarks

If the input byte array is smaller than the configured minimum size then the same data is returned without modification.

### CompressStream(inputStream, outputStream)

Compresses the input stream and writes the Deflate compressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be compressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the compressed data must be written. |

#### Remarks

If the input stream is shorter than the configured minimum size then the same stream data is copied to the output without modification.

### Decompress(source)

Decompresses the given source Deflate byte array and returns a decompressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to decompress. |

#### Returns

Returns a byte array containing the decompressed data.

#### Remarks

If the input byte array does not contain a valid Deflate header then the same data is returned without modification.

### DecompressStream(inputStream, outputStream)

Decompresses the input stream and writes the decompressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be decompressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the decompressed data must be written. |

#### Remarks

If the input stream does not contain a valid Deflate header then the same data is returned without modification.


## GzipCompressor

A compressor which can compress and decompress data in G-Zip format.

### Constructor

Initialises a new instance of the <a href="#gzipcompressor">GzipCompressor</a> class with the default configuration.

### Constructor(configuration)

Initialises a new instance of the <a href="#gzipcompressor">GzipCompressor</a> class with the specified configuration.

| Name | Description |
| ---- | ----------- |
| configuration | *Microsoft.Extensions.Options.IOptions{Trifling.Compression.CompressorConfiguration}*<br>The configuration options for the compression engine. |

### Constructor(configuration)

Initialises a new instance of the <a href="#gzipcompressor">GzipCompressor</a> class with the specified configuration.

| Name | Description |
| ---- | ----------- |
| configuration | *Trifling.Compression.CompressorConfiguration*<br>The configuration options for the compression engine. |

### Compress(source)

Compresses a source byte array and returns a G-Zip compressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to compress. |

#### Returns

Returns a byte array containing the compressed data.

#### Remarks

If the input byte array is smaller than the configured minimum size then the same data is returned without modification.

### CompressStream(inputStream, outputStream)

Compresses the input stream and writes the G-Zip compressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be compressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the compressed data must be written. |

#### Remarks

If the input stream is shorter than the configured minimum size then the same stream data is copied to the output without modification.

### Decompress(source)

Decompresses the given source G-Zip byte array and returns a decompressed byte array.

| Name | Description |
| ---- | ----------- |
| source | *System.Byte[]*<br>The source of data to decompress. |

#### Returns

Returns a byte array containing the decompressed data.

#### Remarks

If the input byte array does not contain a valid G-Zip header then the same data is returned without modification.

### DecompressStream(inputStream, outputStream)

Decompresses the input stream and writes the decompressed stream to the output stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from which to read the data to be decompressed. |
| outputStream | *System.IO.Stream*<br>The stream into which the decompressed data must be written. |

#### Remarks

If the input stream does not contain a valid G-Zip header then the same data is returned without modification.

