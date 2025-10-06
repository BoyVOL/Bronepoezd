using Godot;
using Godot.Collections;
using System;
using MathNet.Numerics;


using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra;


public partial class Sprite2d : Sprite2D
{
	[Export]
	public float sc = 0.5f;
	[Export]
	public float angleX = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	Matrix<float> RotationZ(float angle)
	{
		MatrixBuilder<float> Builder = Matrix<float>.Build;
		Matrix<float> Result = Builder.DenseDiagonal(3, 1);
		Result[0, 0] = (float)Math.Cos(angle);
		Result[0, 1] = -(float)Math.Sin(angle);
		Result[1, 0] = (float)Math.Sin(angle);
		Result[1, 1] = (float)Math.Cos(angle);
		return Result;
	}

	void PassMatrixToShader(float[,] matrix)
	{
		ShaderMaterial ShadMat = Material as ShaderMaterial;
		Vector3 Vec1 = new Vector3(matrix[0, 0], matrix[0, 1], matrix[0, 2]);
		Vector3 Vec2 = new Vector3(matrix[1, 0], matrix[1, 1], matrix[1, 2]);
		Vector3 Vec3 = new Vector3(matrix[2, 0], matrix[2, 1], matrix[2, 2]);
		ShadMat.SetShaderParameter("TMatrixVec1", Vec1);
		ShadMat.SetShaderParameter("TMatrixVec2", Vec2);
		ShadMat.SetShaderParameter("TMatrixVec3", Vec3);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MatrixBuilder<float> matrix = Matrix<float>.Build;
		Matrix<float> M = matrix.DenseDiagonal(3, 1);
		M[2, 2] = sc;
		Matrix<float> Z = RotationZ(angleX);
		Matrix<float> Y = matrix.DenseDiagonal(3, 1);
		Matrix<float> X = matrix.DenseDiagonal(3, 1);
		Matrix<float> result = X * Y * Z*M ;
		//PassMatrixToShader(result.ToArray());
	}
}
