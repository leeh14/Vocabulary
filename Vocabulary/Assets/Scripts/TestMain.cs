using UnityEngine;
using System.Collections;
using live2d;
public class TestMain : MonoBehaviour {
	public TextAsset mocFile;
	public Texture2D[] textures;

	private Live2DModelUnity live2DModel;

	// Use this for initialization
	void Start () {
		Live2D.init ();
		live2DModel = Live2DModelUnity.loadModel (mocFile.bytes);
		for (int i = 0; i < textures.Length; i ++) {
			live2DModel.setTexture(i, textures[i]);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnRenderObject()
	{
		Matrix4x4 m1=Matrix4x4.Ortho(
			-200.0f, 200.0f,
			200.0f,-200.0f,
			-0.5f,0.5f);
		Matrix4x4 m2 = transform.localToWorldMatrix;
		Matrix4x4 m3 = m2*m1;
		
		live2DModel.setMatrix(m3);
		
		live2DModel.update();
		live2DModel.draw();
	}
}
