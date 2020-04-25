using System;
using System.Drawing;
using OpenTK.Input;
using TK = OpenTK;

namespace DotFeather {
/// <summary>
/// <see cref="DotFeather"/> および <see cref="OpenTK"/>
/// 間における類似した型の相互変換をする、拡張メソッドを追加します。
/// </summary>
public static class TypesConverterExtension {
  /// <summary>
  /// <see cref="DotFeather"/> 版に変換します。
  /// </summary>
  public static DFKeyCode ToDF(this Key key) => (DFKeyCode)(int) key;

  /// <summary>
  /// <see cref="OpenTK.Input"/> 版に変換します。
  /// </summary>
  public static Key ToTK(this DFKeyCode key) => (Key)(int) key;
}
}
