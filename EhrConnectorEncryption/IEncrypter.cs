using System;
using System.Collections.Generic;

namespace EhrConnectorEncryption
{
    public interface IEncrypter<T>
    {
        string encrypt(T decrypted);

        T decrypt(string encrypted);
    }
}
