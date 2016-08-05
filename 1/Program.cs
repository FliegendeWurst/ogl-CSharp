using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing.Imaging;
using System.Text;

namespace Example1
{
	class Program
	{
		[STAThread]
		public static void Main()
		{

			using (var Game = new GameWindow())
			{
				int ProgramID = 0;

				int VertexBuffer = 0;
				
				string VertexShaderFilePath = "vs.glsl";
				string FragmentShaderFilePath = "fs.glsl";

				Game.Load += (sender, e) =>
				{
					GL.ClearColor(0.0f, 0.0f, 0.4f, 0.0f);

					// setup settings, load textures, sounds
					int VertexArrayID;
					GL.GenVertexArrays(1, out VertexArrayID);
					GL.BindVertexArray(VertexArrayID);

					ProgramID = Util.LoadShaders(VertexShaderFilePath,
						FragmentShaderFilePath);

					Vector2[] vertices = new Vector2[4];
					vertices[0] = new Vector2(-0.5f, 0.5f);
					vertices[1] = new Vector2(-0.5f, -0.5f);
					vertices[2] = new Vector2(0.5f, -0.5f);
					vertices[3] = new Vector2(0.5f, 0.5f);

					GL.GenBuffers(1, out VertexBuffer);
					GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
					GL.BufferData(BufferTarget.ArrayBuffer,
						new IntPtr(vertices.Length * Vector2.SizeInBytes),
						vertices, BufferUsageHint.StaticDraw);

					GL.UseProgram(ProgramID);

					Game.VSync = VSyncMode.On;
				};

				Game.Resize += (sender, e) =>
				{
					GL.Viewport(0, 0, Game.Width, Game.Height);
				};

				Game.UpdateFrame += (sender, e) =>
				{
					// add game logic, input handling
					if (Keyboard.GetState().IsKeyDown(Key.Escape))
					{
						Game.Exit();
					}
				};

				Game.RenderFrame += (sender, e) =>
				{
					GL.Clear(ClearBufferMask.ColorBufferBit);

					GL.EnableVertexAttribArray(0);
					GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
					GL.VertexAttribPointer(
						0,
						2,
						VertexAttribPointerType.Float,
						false,
						0,
						0
					);

					GL.DrawArrays(PrimitiveType.Quads, 0, 4);
					GL.DisableVertexAttribArray(0);

					Game.SwapBuffers();
				};

				// Run the game at 60 updates per second
				Game.Run(60.0);
			}
		}
	}
}