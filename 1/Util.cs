using OpenTK.Graphics.OpenGL;
using System;
using System.Text;

namespace Example1
{
	class Util
	{
		public static int LoadShaders(string VertexFilePath, string FragmentFilePath)
		{
			int ProgramID = GL.CreateProgram();

			// Create the shaders
			int VertexShaderID = GL.CreateShader(ShaderType.VertexShader);
			int FragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);

			// Read the Vertex Shader code from the file
			string VertexShaderCode = System.IO.File.ReadAllText(VertexFilePath);

			// Read the Fragment Shader code from the file
			string FragmentShaderCode = System.IO.File.ReadAllText(FragmentFilePath);

			// Compile Vertex Shader
			GL.ShaderSource(VertexShaderID, VertexShaderCode);
			GL.CompileShader(VertexShaderID);

			int Result;
			int InfoLogLength;

			// Check Vertex Shader
			GL.GetShader(VertexShaderID, ShaderParameter.CompileStatus, out Result);
			GL.GetShader(VertexShaderID, ShaderParameter.InfoLogLength, out InfoLogLength);
			if (InfoLogLength > 1)
			{
				Console.WriteLine("Compiling vertex shader..");
				StringBuilder SB = new StringBuilder();
				GL.GetShaderInfoLog(VertexShaderID, InfoLogLength, out InfoLogLength, SB);
				Console.WriteLine("Compile status: " + (Result != 0));
				Console.Write(SB.ToString());
			}

			// Compile Fragment Shader

			GL.ShaderSource(FragmentShaderID, FragmentShaderCode);
			GL.CompileShader(FragmentShaderID);

			Result = 0;
			InfoLogLength = 0;

			// Check Fragment Shader
			GL.GetShader(VertexShaderID, ShaderParameter.CompileStatus, out Result);
			GL.GetShader(VertexShaderID, ShaderParameter.InfoLogLength, out InfoLogLength);
			if (InfoLogLength > 1)
			{
				Console.WriteLine("Compiling fragment shader..");
				StringBuilder sb = new StringBuilder();
				GL.GetShaderInfoLog(FragmentShaderID, InfoLogLength, out InfoLogLength, sb);
				Console.WriteLine("Compile status: " + (Result != 0));
				Console.Write(sb.ToString());
			}

			// Link the program
			GL.AttachShader(ProgramID, VertexShaderID);
			GL.AttachShader(ProgramID, FragmentShaderID);
			GL.LinkProgram(ProgramID);

			GL.DetachShader(ProgramID, VertexShaderID);
			GL.DetachShader(ProgramID, FragmentShaderID);


			GL.DeleteShader(VertexShaderID);
			GL.DeleteShader(FragmentShaderID);

			return ProgramID;
		}
	}
}
