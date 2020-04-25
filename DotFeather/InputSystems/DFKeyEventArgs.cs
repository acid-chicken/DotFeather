using System;

namespace DotFeather {
/// <summary>
/// Keyboard event argument.
/// </summary>
public class DFKeyEventArgs : EventArgs {
  /// <summary>
  /// Get a pressed key.
  /// </summary>
  public DFKeyCode Key { get; }

  /// <summary>
  /// Gets a value indicating whether the Alt key was pressed.
  /// </summary>
  public bool AltPressed { get; }

  /// <summary>
  /// Gets a value indicating whether the Ctrl key was pressed.
  /// </summary>
  public bool CtrlPressed { get; }

  /// <summary>
  /// Gets a value indicating whether the Shift key was pressed.
  /// </summary>
  public bool ShiftPressed { get; }

  internal DFKeyEventArgs(OpenTK.Input.KeyboardKeyEventArgs e) {
    Key = e.Key.ToDF();
    AltPressed = e.Alt;
    CtrlPressed = e.Control;
    ShiftPressed = e.Shift;
  }
}
}
