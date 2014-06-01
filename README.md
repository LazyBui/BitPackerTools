BitPackerTools
==============

A .NET library designed to work with streams that are bit packed.

Bit packing is the practice of occupying minimum space in a stream (for example, perhaps a network stream) by reducing a value to the maximum number of bits that it may use.

For example, if you have 3 separate values, the lowest amount of space you can take is 3 bytes. But perhaps you have high level information about these values such that you know that 2 of them have a maximum value of 7 and the third has a maximum value of 3. This means that you can use a single byte to represent all 3 values.

Make no mistake that this is much slower in terms of CPU time than just using bytes or any other kind of value. However, from time to time, you may be interacting with a legacy system or protocol that requires this type of functionality and .NET provides no good solution for this. You must write your own.

This library seeks to mimic the serialization interface of other classes, such as XmlSerializer in System.Xml.Serialization and provide similar facilities for control over de/serialization.
