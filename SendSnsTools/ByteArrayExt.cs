namespace SendSnsTools
{
    internal static class ByteArrayExt
    {
        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream ToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
