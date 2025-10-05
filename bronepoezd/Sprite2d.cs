using Godot;
using Godot.Collections;
using System;
using MathNet.Numerics;


using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra;

[Tool]
public partial class Sprite2d : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	void PassMatrixToShader(float[,] matrix)
	{
		ShaderMaterial ShadMat = Material as ShaderMaterial;
		ShadMat.SetShaderParameter("TMatrixVec1", new Vector3(matrix[0,0], matrix[0, 1], matrix[0, 2]));
		ShadMat.SetShaderParameter("TMatrixVec2", new Vector3(matrix[1, 0], matrix[1, 1], matrix[1, 2]));
		ShadMat.SetShaderParameter("TMatrixVec3", new Vector3(matrix[2, 0], matrix[2, 1], matrix[2, 2]));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var matrix = Matrix<float>.Build;
		var M = matrix.DenseDiagonal(3, 1);
		M[2, 2] = 0.5f;
		PassMatrixToShader(M.ToArray());
	}
}
