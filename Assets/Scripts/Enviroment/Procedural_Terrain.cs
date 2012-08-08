using UnityEngine;
using System.Collections;

public class Procedural_Terrain : MonoBehaviour 
{
	void calculateMeshTangents(Mesh mesh)
	{
	    //speed up math by copying the mesh arrays
	    int[] triangles = mesh.triangles;
	    Vector3[] vertices = mesh.vertices;
	    Vector2[] uv = mesh.uv;
	    Vector3[] normals = mesh.normals;
	
	    //variable definitions
	    int triangleCount = triangles.Length;
	    int vertexCount = vertices.Length;
	
	    Vector3[] tan1 = new Vector3[vertexCount];
	    Vector3[] tan2 = new Vector3[vertexCount];
	
	    Vector4[] tangents = new Vector4[vertexCount];
	
	    for (long a = 0; a < triangleCount; a += 3)
	    {
	        long i1 = triangles[a + 0];
	        long i2 = triangles[a + 1];
	        long i3 = triangles[a + 2];
	
	        Vector3 v1 = vertices[i1];
	        Vector3 v2 = vertices[i2];
	        Vector3 v3 = vertices[i3];
	
	        Vector2 w1 = uv[i1];
	        Vector2 w2 = uv[i2];
	        Vector2 w3 = uv[i3];
	
	        float x1 = v2.x - v1.x;
	        float x2 = v3.x - v1.x;
	        float y1 = v2.y - v1.y;
	        float y2 = v3.y - v1.y;
	        float z1 = v2.z - v1.z;
	        float z2 = v3.z - v1.z;
	
	        float s1 = w2.x - w1.x;
	        float s2 = w3.x - w1.x;
	        float t1 = w2.y - w1.y;
	        float t2 = w3.y - w1.y;
	
	        float r = 1.0f / (s1 * t2 - s2 * t1);
	
	        Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
	        Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
	
	        tan1[i1] += sdir;
	        tan1[i2] += sdir;
	        tan1[i3] += sdir;
	
	        tan2[i1] += tdir;
	        tan2[i2] += tdir;
	        tan2[i3] += tdir;
	    }
	
	    for (long a = 0; a < vertexCount; ++a)
	    {
	        Vector3 n = normals[a];
	        Vector3 t = tan1[a];
	
	        //Vector3 tmp = (t - n * Vector3.Dot(n, t)).normalized;
	        //tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
	        Vector3.OrthoNormalize(ref n, ref t);
	        tangents[a].x = t.x;
	        tangents[a].y = t.y;
	        tangents[a].z = t.z;
	
	        tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
	    }
	
	    mesh.tangents = tangents;
	}
	
	float TerrainY( Vector3 p, int ioct )
	{
		float x = ( p.x + 300.0f );
		float y = ( p.z + 300.0f );
		
	    float f = 0.0f;
	    float w = 1.0f;
		Vector2 c = new Vector2( 0.0f, 0.0f );
		
	    for( int i = 0; i < ioct ; i++ )
	    {
	        float n = Mathf.PerlinNoise( x, y );
			
			Random.seed = (int)( x + y );
			Vector2 r1 = Random.insideUnitCircle;
			Random.seed++;
			Vector2 r2 = Random.insideUnitCircle;
			
	        float n2 = Mathf.PerlinNoise( r1.x, r1.y );
	        float n3 = Mathf.PerlinNoise( r2.x, r2.y );
			
			c += new Vector2( n2, n3 );
			
	        f += w * n / ( 1.0f + Vector2.Dot( c, c ) );
			
			w *= 0.5f;
			x *= 2.0f;
			y *= 2.0f;
	    }

		return f;
	}
	
	Vector3 CalcNormal( Vector3 a, Vector3 b, Vector3 c )
	{
		return Vector3.Cross( a, b ) + Vector3.Cross( b, c ) + Vector3.Cross( c, a );
	}
	
	void Start( ) 
	{
        Mesh mesh = GetComponent<MeshFilter>().mesh;
		
        Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
        Vector3[] normals = mesh.normals;
		
        for( int i = 0; i < vertices.Length; i++ ) 
            vertices[i].y = TerrainY( vertices[i], 16 );
		
        for( int i = 0; i < normals.Length; i++ ) 
			normals[i] = Vector3.zero;
		
		int f = 0;
        for( int i = 0; i < triangles.Length / 3; i++ ) 
		{
			Vector3 trinormal = CalcNormal( 
				vertices[triangles[f]], 
				vertices[triangles[f+1]], 
				vertices[triangles[f+2]] 
			);
			
			normals[triangles[f]] += trinormal;
			normals[triangles[f+1]] += trinormal;
			normals[triangles[f+2]] += trinormal;
			
			f+=3;
		}
		
        for( int i = 0; i < normals.Length; i++ ) 
			normals[i] = Vector3.Normalize( normals[i] );
		
        mesh.vertices = vertices;
        mesh.normals = normals;
		
		calculateMeshTangents( mesh );
	}
}
