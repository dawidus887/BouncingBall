using BouncingBall.App;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var windowSettings = GameWindowSettings.Default;
windowSettings.IsMultiThreaded = false;
windowSettings.RenderFrequency = 60;
windowSettings.UpdateFrequency = 60;

var nativeSettings = NativeWindowSettings.Default;
nativeSettings.IsEventDriven = true;
nativeSettings.API = ContextAPI.OpenGL;
nativeSettings.APIVersion = Version.Parse("4.1");
nativeSettings.Size = new Vector2i(1280, 720);
nativeSettings.Title = "Bouncing Ball";

var window = new GameWindow(windowSettings, nativeSettings);

//event triggered every time the window is updated with new frame (so 60hz)
window.UpdateFrame += eventArgs => { };

var shaderProgram = new ShaderProgram();

//loading shaders
window.Load += () =>
{
    shaderProgram = Utilities.LoadShaderProgram("../../../Shaders/vertex_shader.glsl",
        "../../../Shaders/fragment_shader.glsl");
};

window.RenderFrame += eventArgs =>
{
    GL.UseProgram(shaderProgram.Id);

    GL.ClearColor(1, 0.0f, 0.0f, 0);
    GL.Clear(ClearBufferMask.ColorBufferBit);

    #region Triangle Building

    float[] verts =
    {
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f, 0.5f, 0.0f
    };

    var vao = GL.GenVertexArray();
    var vertices = GL.GenBuffer();

    GL.BindVertexArray(vao);
    GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
    GL.BufferData(BufferTarget.ArrayBuffer, 36, verts, BufferUsageHint.StaticDraw);
    GL.EnableVertexAttribArray(0);
    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    GL.BindVertexArray(0);
    GL.DeleteVertexArray(vao);
    GL.DeleteBuffer(vertices);

    #endregion

    window.SwapBuffers();
};

window.Run();