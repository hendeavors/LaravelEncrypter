using System;
using System.Collections.Generic;

namespace LaravelEncrypter
{
    public interface IEncrypter<T>
    {
        string encrypt(T decrypted);

        T decrypt(string encrypted);
    }
}
