# Trifling.Serialization

## IBinarySerializer

A binary serialiser to convert values to byte arrays.

### Deserialize\<T\>(value)

Converts the given bytes to the type T.

#### Type Parameters

- T - The expected type that should be returned from the given byte array.

| Name | Description |
| ---- | ----------- |
| value | *System.Byte[]*<br>The byte data that should be decoded as an instance of an object of type T. |

#### Returns

Returns the de-serialised value.

### DeserializeFromStream\<T\>(inputStream)

Reads bytes from the inputStream and converts the data to an object of type T.

#### Type Parameters

- T - The expected type that should be read from the input stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from where the source of bytes should be read. |

#### Returns

Returns the de-serialised value.

### Serialize\<T\>(value)

Converts the given object instance into a byte array.

#### Type Parameters

- T - The type of object that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded in a byte array. |

#### Returns

Returns the serialised value as a byte array.

### SerializeToStream\<T\>(value, outputStream)

Converts the given object instance into a byte array and writes the output to the given stream.

#### Type Parameters

- T - The type of object that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded and written to the outputStream. |
| outputStream | *System.IO.Stream*<br>The stream into which the output should be written. |


## BsonSerializer

An implementation of a binary serialiser which uses Newtonsoft to create BSON data.

### Deserialize\<T\>(value)

Converts the given bytes to a value of type T (assuming that the data is BSON compatible).

#### Type Parameters

- T - The expected type that should be returned from the given byte data.

| Name | Description |
| ---- | ----------- |
| value | *System.Byte[]*<br>The byte data that should be decoded as BSON into an instance of type T. |

#### Returns

Returns the de-serialised value.

### DeserializeFromStream\<T\>(inputStream)

Reads bytes from the inputStream and converts the data to an object of type T.

#### Type Parameters

- T - The expected type that should be read from the input stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from where the source of bytes should be read. |

#### Returns

Returns the de-serialised value.

### MakeBsonCompatibleObject\<T\>(value)

Converts the given value to a value that can be serialised to BSON.

#### Type Parameters

- T - The type of the value that is being serialised.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The value which will be serialised. |

#### Returns

Returns an object that can be serialised to BSON.

### Serialize\<T\>(value)

Converts the given object instance into a BSON compatible byte array.

#### Type Parameters

- T - The object type that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded as BSON data. |

#### Returns

Returns the serialised value as a byte array.

### SerializeToStream\<T\>(value, outputStream)

Converts the given object instance into a byte array and writes the output to the given stream.

#### Type Parameters

- T - The type of object that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded and written to the outputStream. |
| outputStream | *System.IO.Stream*<br>The stream into which the output should be written. |


## BsonWrappedType\<T\>

A class used to wrap simple struct values as BSON-compatible objects.

#### Type Parameters

- T - The simple type that is being wrapped.

### Constructor(value)

Initialises a new instance of the <a href="#bsonwrappedtype">BsonWrappedType\<T\></a> class with the given value.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The value being wrapped. |

### Value

Gets or sets the value being wrapped.


## DotnetSerializer

This class has not been implemented because NetStandard 1.6 does not support this operation.

### Deserialize\<T\>(value)

Converts the given bytes to the type T.

#### Type Parameters

- T - The expected type that should be returned from the given byte array.

| Name | Description |
| ---- | ----------- |
| value | *System.Byte[]*<br>The byte data that should be decoded as an instance of an object of type T. |

#### Returns

Returns the de-serialised value.

*System.NotImplementedException:* This exception is always returned.

### DeserializeFromStream\<T\>(inputStream)

Reads bytes from the inputStream and converts the data to an object of type T.

#### Type Parameters

- T - The expected type that should be read from the input stream.

| Name | Description |
| ---- | ----------- |
| inputStream | *System.IO.Stream*<br>The stream from where the source of bytes should be read. |

#### Returns

Returns the de-serialised value.

*System.NotImplementedException:* This exception is always returned.

### Serialize\<T\>(value)

Converts the given object instance into a byte array.

#### Type Parameters

- T - The type of object that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded in a byte array. |

#### Returns

Returns the serialised value as a byte array.

*System.NotImplementedException:* This exception is always returned.

### SerializeToStream\<T\>(value, outputStream)

Converts the given object instance into a byte array and writes the output to the given stream.

#### Type Parameters

- T - The type of object that has been given.

| Name | Description |
| ---- | ----------- |
| value | *T*<br>The object instance that should be encoded and written to the outputStream. |
| outputStream | *System.IO.Stream*<br>The stream into which the output should be written. |

*System.NotImplementedException:* This exception is always returned.

