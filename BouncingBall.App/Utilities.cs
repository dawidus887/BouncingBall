using OpenTK.Graphics.OpenGL4;

namespace BouncingBall.App;

public static class Utilities
{
    public static ShaderProgram LoadShaderProgram(string vertexShaderLocation, string fragmentShaderLocation)
    {
        var shaderProgramId = GL.CreateProgram();

        var vertexShader = LoadShader(vertexShaderLocation, ShaderType.VertexShader);
        var fragmentShader = LoadShader(fragmentShaderLocation, ShaderType.FragmentShader);

        GL.AttachShader(shaderProgramId, vertexShader.Id);
        GL.AttachShader(shaderProgramId, fragmentShader.Id);
        GL.LinkProgram(shaderProgramId);

        GL.DetachShader(shaderProgramId, vertexShader.Id);
        GL.DetachShader(shaderProgramId, fragmentShader.Id);
        GL.DeleteShader(vertexShader.Id);
        GL.DeleteShader(fragmentShader.Id);

        var infoLog = GL.GetProgramInfoLog(shaderProgramId);

        if (!string.IsNullOrEmpty(infoLog))
            throw new Exception(infoLog);

        return new ShaderProgram(shaderProgramId);
    }

    private static Shader LoadShader(string shaderLocation, ShaderType type)
    {
        var shaderId = GL.CreateShader(type);
        GL.ShaderSource(shaderId, File.ReadAllText(shaderLocation));
        GL.CompileShader(shaderId);
        var infoLog = GL.GetShaderInfoLog(shaderId);

        if (!string.IsNullOrEmpty(infoLog))
            throw new Exception(infoLog);

        return new Shader(shaderId);
    }
}