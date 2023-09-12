using System.IO;

class Program {
  private static void Main(string[] args) {
    if (!args.Any()) throw new Exception("Arguments Lack File Path");

    string path = args[0];
    if (!File.Exists(path)) throw new Exception("File Does Not Exist");

    TextReader input = File.OpenText(path);

    byte[] arr = BFExecuter(input.ToString().ToCharArray());

    PrintNElementsOfArrayAsHex(arr, 5);
  }

  private static void PrintNElementsOfArrayAsHex(byte[] arr, int n) {
    Console.Write("[");
    for (byte i = 0; i < n; i++) {
      Console.Write($"0x{arr[i]:X2}, ");
    }
    Console.Write($"0x{arr[n]:X2}");
    Console.WriteLine("]");
  }

  private static byte[] BFExecuter(char[] chars) {
    byte[] arr = new byte[byte.MaxValue];
    byte ptr = 0;
    uint charPtr = 0;

    Stack<byte> bracketPtrs = new Stack<byte>();

    while(charPtr < chars.Length) {
      char c = chars[charPtr];
      switch (c) {
        case '+':
          if (arr[ptr] == byte.MaxValue)
            arr[ptr] = byte.MinValue;
          else
            arr[ptr]++;
          break;
        case '-':
          if (arr[ptr] == byte.MinValue)
            arr[ptr] = byte.MaxValue;
          else
            arr[ptr]--;
          break;
        case '>':
          if (ptr == byte.MaxValue)
            ptr = byte.MinValue;
          else
            ptr++;
          break;
        case '<':
          if (ptr == byte.MinValue)
            ptr = byte.MaxValue;
          else
            ptr--;
          break;
        case '.':
          Console.Write(Convert.ToChar(arr[ptr]));
          break;
        case ',':
          arr[ptr] = Convert.ToByte(Console.Read());
          break;
        case '[':
          if (arr[ptr] != 0) {
            bracketPtrs.Push(ptr);
            break;
          }

          while (true) {
            charPtr++;
            char c2 = chars[charPtr];
            if (c2 == ']') break;
          }
          break;
        case ']':
          if (arr[ptr] == 0) 
            break;
          ptr = bracketPtrs.Pop();
          break;
      }
    }

    Console.WriteLine();

    return arr;
  }
}
