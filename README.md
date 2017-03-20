# trifling.common
A Trifling Library of common functionality.
=======
# Trifling Common

[![Build Status](https://travis-ci.org/JamesHough/trifling.common.svg?branch=master)](https://travis-ci.org/JamesHough/trifling.common)

A Trifling library of common functionality.

This library includes a set of interfaces for serialisation and compression.

| Namespace | Interface |
|-----------|-----------|
| Trifling.Serialization.Interfaces | [IBinarySerializer](docs/trifling.serialization.md) |
| Trifling.Compression.Interfaces | [ICompressor](docs/trifling.compression.md) |
| Trifling.Compression.Interfaces | [IDeflateCompressor](docs/trifling.compression.md) |
| Trifling.Compression.Interfaces | [IGzipCompressor](docs/trifling.compression.md) |
| Trifling.Compression.Interfaces | [ICompressorFactory](docs/trifling.compression.md) |

---
It also includes implementations of those interfaces.

| Interface | Namespace | Implementation |
|-----------|-----------|----------------|
| IBinarySerializer | Trifling.Serialization.Impl | [BsonSerializer](docs/trifling.serialization.md) |
| IBinarySerializer | Trifling.Serialization.Impl | [DotnetSerializer](docs/trifling.serialization.md) `not implemented in NetStandard 1.6` |
| IDeflateCompressor | Trifling.Compression.Impl | [DeflateCompressor](docs/trifling.compression.md) |
| IGZipCompressor | Trifling.Compression.Impl | [GZipCompressor](docs/trifling.compression.md) |
| ICompressorFactory | Trifling.Compression.Factory | [CompressorFactory](docs/trifling.compression.md) |
| ICompressorFactory | Trifling.Compression.Factory | [CachedCompressorFactory](docs/trifling.compression.md) |

---
The library includes some utilities and components for comparison and logging.

| Namespace | Utility / Component |
|-----------|---------------------|
| Trifling.Comparison | [ByteArrayComparer](docs/trifling.bytearraycomparer.md) |
| Trifling.Comparison | [BoxedByteArrayComparer](docs/trifling.bytearraycomparer.md) |
| Trifling.Compression | [CompressorConfiguration](docs/trifling.compression.md) |
| Trifling.Logging | [LoggingTextWriter](docs/trifling.utility.md) |

---
