using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotFeather
{
	public static class DF
	{
		/// <summary>
		/// Provides Low-level Texture Drawing API.
		/// </summary>
		public static ITextureDrawer TextureDrawer { get; }

		/// <summary>
		/// Provides Low-level Primitive Drawing API.
		/// </summary>
		public static IPrimitiveDrawer PrimitiveDrawer { get; }

		/// <summary>
		/// Provides Windowing API.
		/// </summary>
		public static IWindow Window { get; }

		/// <summary>
		/// Provides Console API.
		/// </summary>
		public static IConsole Console { get; }

		/// <summary>
		/// Get root level elements.
		/// </summary>
		public static Container Root { get; private set; } = new Container();

		/// <summary>
		/// Provides Router API.
		/// </summary>
		public static Router Router { get; }

		/// <summary>
		/// Open and run DotFeather Window.
		/// </summary>
		public static int Run()
		{
			Window.Run();
			return statusCode;
		}

		/// <summary>
		/// Close the window and exit this program.
		/// </summary>
		/// <param name="status">A status code. It will be a result of a <see cref="Run"/>() method.</param>
		public static void Exit(int status = 0)
		{
			statusCode = status;
			Window.Exit();
		}

		/// <summary>
		/// Not implemented yet, so this works as same as <see cref="Run"/>() method.
		/// </summary>
		public static int RunAsCaptureMode()
		{
			// todo
			return Run();
		}

		/// <summary>
		/// Register a task for the next frame.
		/// </summary>
		/// <param name="task">A task to register.</param>
		public static void NextFrame(Action task)
		{
			nextFrameQueue.Add(task);
		}

		static DF()
		{
			ctx = new DFSynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(ctx);


			// Add Plugins
			TextureDrawer = new Internal.DesktopTextureDrawer();
			PrimitiveDrawer = new Internal.DesktopPrimitiveDrawer();

			Window = new Internal.DesktopWindow();

			Console = new Internal.DFConsole();
			Router = new Router();

			Window.Update += () =>
			{
				ctx.Update();

				if (nextFrameQueue.Count == 0) return;
				nextFrameQueue.ToList().ForEach(task =>
				{
					task();
					nextFrameQueue.Remove(task);
				});
			};
		}

		private static readonly List<Action> nextFrameQueue = new();
		private static readonly DFSynchronizationContext ctx;
		private static int statusCode;
	}
}
