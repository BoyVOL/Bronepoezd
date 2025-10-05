shader_type canvas_item;

uniform vec3 TMatrixVec1;
uniform vec3 TMatrixVec2;
uniform vec3 TMatrixVec3;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	mat3 TMatrix = mat3(TMatrixVec1,TMatrixVec2,TMatrixVec3);
	// Called for every pixel the material is visible on.
	vec3 uv = TMatrix * vec3(UV - 0.5,1.0) ;
	if(uv.z < 0.0) discard;
	COLOR = texture(TEXTURE,(uv.xy/uv.z)+0.5);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
