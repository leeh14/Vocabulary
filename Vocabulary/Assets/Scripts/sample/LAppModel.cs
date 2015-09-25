using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using live2d.framework;
using live2d;


public class LAppModel :L2DBaseModel
{
    private LAppModelProxy parent;
    private LAppView view;

    
    private String modelHomeDir;
    private ModelSetting modelSetting = null;	

	private Matrix4x4 matrixHitArea; 
    

    
    private AudioSource asVoice;

    System.Random rand = new System.Random();

    private Bounds bounds;




    public LAppModel(LAppModelProxy p)
    {
        if (isInitialized()) return;
        parent = p;
		
		if (parent.GetComponent<AudioSource>() != null)
		{
			asVoice = parent.gameObject.GetComponent<AudioSource>();
            asVoice.playOnAwake = false;
        }
        else
        {
            if (LAppDefine.DEBUG_LOG)
            {
                Debug.Log("Live2D : AudioSource Component is NULL !");
            }
        }

        bounds = parent.GetComponent<MeshFilter>().sharedMesh.bounds;

        view = new LAppView(this, parent.transform);
        view.StartAccel();


        //if (LAppDefine.DEBUG_LOG) mainMotionManager.setMotionDebugMode(true);
    }


    public void LoadFromStreamingAssets(String dir,String filename)
    {
        if (LAppDefine.DEBUG_LOG) Debug.Log(dir + filename);
        modelHomeDir = dir;
		var data = Live2DFramework.getPlatformManager().loadString(modelHomeDir + filename);
        Init(data);
    }
   

    
    public void Init(String modelJson)
    {
        updating = true;
        initialized = false;

        modelSetting = new ModelSettingJson(modelJson);

        if (LAppDefine.DEBUG_LOG) Debug.Log("Start to load model");


        // Live2D Model
        if (modelSetting.GetModelFile() != null)
        {
            loadModelData(modelHomeDir + modelSetting.GetModelFile());

            var len = modelSetting.GetTextureNum();
            for (int i = 0; i < len; i++)
            {
                loadTexture(i, modelHomeDir + modelSetting.GetTextureFile(i));
            }
        }
       
        // Expression
        if (modelSetting.GetExpressionNum() != 0)
        {
            var len = modelSetting.GetExpressionNum();
            for (int i = 0; i < len; i++)
            {
				loadExpression(modelSetting.GetExpressionName(i), modelHomeDir + modelSetting.GetExpressionFile(i));
            }
        }

        // Physics
        if (modelSetting.GetPhysicsFile()!=null)
        {
            loadPhysics(modelHomeDir + modelSetting.GetPhysicsFile());            
        }

        // Pose
        if (modelSetting.GetPoseFile()!=null)
        {
            loadPose(modelHomeDir + modelSetting.GetPoseFile());    
        }

        
        //Dictionary<string, float> layout = new Dictionary<string, float>();
        //if (modelSetting.GetLayout(layout))
        //{
        //    if (layout.ContainsKey("width")) modelMatrix.setWidth(layout["width"]);
        //    if (layout.ContainsKey("height")) modelMatrix.setHeight(layout["height"]);
        //    if (layout.ContainsKey("x")) modelMatrix.setX(layout["x"]);
        //    if (layout.ContainsKey("y")) modelMatrix.setY(layout["y"]);
        //    if (layout.ContainsKey("center_x")) modelMatrix.centerX(layout["center_x"]);
        //    if (layout.ContainsKey("center_y")) modelMatrix.centerY(layout["center_y"]);
        //    if (layout.ContainsKey("top")) modelMatrix.top(layout["top"]);
        //    if (layout.ContainsKey("bottom")) modelMatrix.bottom(layout["bottom"]);
        //    if (layout.ContainsKey("left")) modelMatrix.left(layout["left"]);
        //    if (layout.ContainsKey("right")) modelMatrix.right(layout["right"]);
        //}


        
        for (int i = 0; i < modelSetting.GetInitParamNum(); i++)
        {
            string id = modelSetting.GetInitParamID(i);
            float value = modelSetting.GetInitParamValue(i);
            live2DModel.setParamFloat(id, value);
        }

        for (int i = 0; i < modelSetting.GetInitPartsVisibleNum(); i++)
        {
            string id = modelSetting.GetInitPartsVisibleID(i);
            float value = modelSetting.GetInitPartsVisibleValue(i);
            live2DModel.setPartsOpacity(id, value);
        }

        
        eyeBlink = new L2DEyeBlink();
        view.SetupView(
            live2DModel.getCanvasWidth(),
            live2DModel.getCanvasHeight());

        updating = false;
        initialized = true;
    }


    
    public void Update()
    {
        if ( ! isInitialized() || isUpdating())
        {
            return;
        }


        view.Update(Input.acceleration);
        if (live2DModel == null)
        {
            if (LAppDefine.DEBUG_LOG) Debug.Log("Can not update there is no model data");
            return;
        }

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        long timeMSec = UtSystem.getUserTimeMSec() - startTimeMSec;
        double timeSec = timeMSec / 1000.0;
        double t = timeSec * 2 * Math.PI;

        
        //if (mainMotionManager.isFinished())
        //{   
         //   StartRandomMotion(LAppDefine.MOTION_GROUP_IDLE, LAppDefine.PRIORITY_IDLE);
        //}

		if (Input.GetButton ("Fire1") && mainMotionManager.isFinished()) 
		{
			if(TapEvent(Input.mousePosition.x,Input.mousePosition.y) == true)
			{
				Debug.Log("tap hit");
				int max = modelSetting.GetMotionNum(LAppDefine.MOTION_GROUP_SHAKE);
				int no = (int)(rand.NextDouble() * max);
				StartMotion(LAppDefine.MOTION_GROUP_SHAKE, no, 1);
			}
			//Debug.Log(Input.mousePosition.x + Input.mousePosition.y);
			if(HitTest(LAppDefine.HIT_AREA_HEAD,Input.mousePosition.x,Input.mousePosition.y))
			{
				Debug.Log("ahit");
				int max = modelSetting.GetMotionNum(LAppDefine.MOTION_GROUP_SHAKE);
				int no = (int)(rand.NextDouble() * max);
				StartMotion(LAppDefine.MOTION_GROUP_SHAKE, no, 1);
			}

		}
        //-----------------------------------------------------------------
        live2DModel.loadParam();

        bool update = mainMotionManager.updateParam(live2DModel);

        if (!update)
        {
            
            eyeBlink.updateParam(live2DModel);
        }

        live2DModel.saveParam();
        //-----------------------------------------------------------------

        if (expressionManager != null) expressionManager.updateParam(live2DModel);


        
        
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_X, dragX * 30, 1);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Y, dragY * 30, 1);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Z, (dragX * dragY) * -30, 1);

        
        live2DModel.addToParamFloat(L2DStandardID.PARAM_BODY_ANGLE_X, dragX, 10);

        
        live2DModel.addToParamFloat(L2DStandardID.PARAM_EYE_BALL_X, dragX, 1);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_EYE_BALL_Y, dragY, 1);

        
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_X, (float)(15 * Math.Sin(t / 6.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Y, (float)(8 * Math.Sin(t / 3.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Z, (float)(10 * Math.Sin(t / 5.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_BODY_ANGLE_X, (float)(4 * Math.Sin(t / 15.5345)), 0.5f);
        live2DModel.setParamFloat(L2DStandardID.PARAM_BREATH, (float)(0.5f + 0.5f * Math.Sin(t / 3.2345)), 1);


        
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_X, 90 * accelX, 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Z, 10 * accelX, 0.5f);


        if (physics != null) physics.updateParam(live2DModel);

        
        if (lipSync)
        {
            live2DModel.setParamFloat(L2DStandardID.PARAM_MOUTH_OPEN_Y, lipSyncValue, 0.8f);
        }

        
        if (pose != null) pose.updateParam(live2DModel);

        live2DModel.update();
    }


    public void Draw()
    {
        Matrix4x4 planeLocalToWorld = parent.transform.localToWorldMatrix;

        
        Matrix4x4 rotateModelOnToPlane = Matrix4x4.identity;
        rotateModelOnToPlane.SetTRS(Vector3.zero, Quaternion.Euler(90, 0, 0), Vector3.one);

        Matrix4x4 scale2x2ToPlane = Matrix4x4.identity;
        
        Vector3 scale = new Vector3(bounds.size.x / 2.0f, -1, bounds.size.z / 2.0f);
        scale2x2ToPlane.SetTRS(Vector3.zero, Quaternion.identity, scale);

        
        Matrix4x4 modelMatrix4x4 = Matrix4x4.identity;
        float[] matrix = modelMatrix.getArray();
        for (int i = 0; i < 16; i++)
        {
            modelMatrix4x4[i] = matrix[i];
        }

        Matrix4x4 modelCanvasToWorld = planeLocalToWorld * scale2x2ToPlane * rotateModelOnToPlane * modelMatrix4x4;

        GetLive2DModelUnity().setMatrix(modelCanvasToWorld);

        live2DModel.draw();

		matrixHitArea = modelCanvasToWorld;
    }


    
    public void DrawHitArea()
    {
        
        int len = modelSetting.GetHitAreasNum();
        for (int i = 0; i < len; i++)
        {
            string drawID = modelSetting.GetHitAreaID(i);
            float left = 0;
            float right = 0;
            float top = 0;
            float bottom = 0;

            if (!getSimpleRect(drawID, out left, out right, out top, out bottom))
            {
                continue;
            }

			HitAreaUtil.DrawRect(matrixHitArea,left, right, top, bottom);
        }
    }


    public void StartRandomMotion(string name, int priority)
    {
        int max = modelSetting.GetMotionNum(name);
        int no = (int)(rand.NextDouble() * max);
        StartMotion(name, no, priority);
    }


    
    public void StartMotion(string group, int no, int priority)
    {
        string motionName = modelSetting.GetMotionFile(group, no);

        if (motionName == null || motionName.Equals(""))
        {
            if (LAppDefine.DEBUG_LOG) Debug.Log("Motion name is invalid");
            return;//
        }

        
        
        //
        
        
        if (priority == LAppDefine.PRIORITY_FORCE)
        {
            mainMotionManager.setReservePriority(priority);
        }
        else if (!mainMotionManager.reserveMotion(priority))
        {
            if (LAppDefine.DEBUG_LOG) { Debug.Log("Do not play because book already playing, or playing a motion already." + motionName); }
            return;
        }

        AMotion motion=null;
        string name = group + "_" + no;

        if (motions.ContainsKey(name))
        {
            motion = motions[name];            
        }
        if (motion==null)
        {
            motion = loadMotion(name, modelHomeDir+motionName);
        }
        if (motion == null)
        {
            Debug.Log("Failed to read the motion."+motionName);
            mainMotionManager.setReservePriority(0);
            return;
        }

        
        motion.setFadeIn(modelSetting.GetMotionFadeIn(group, no));
        motion.setFadeOut(modelSetting.GetMotionFadeOut(group, no));


        if ((modelSetting.GetMotionSound(group, no)) == null)
        {
            
            if (LAppDefine.DEBUG_LOG) Debug.Log("Start motion : " + motionName);
            mainMotionManager.startMotionPrio(motion, priority);
        }
        else
        {
            
            string soundPath = modelSetting.GetMotionSound(group, no);
            soundPath = Regex.Replace(soundPath, ".mp3$", "");

            AudioClip acVoice = FileManager.LoadAssetsSound(modelHomeDir+soundPath);
            if (LAppDefine.DEBUG_LOG) Debug.Log("Start motion : " + motionName + "  voice : " + soundPath);
            StartVoice( acVoice);
            mainMotionManager.startMotionPrio(motion, priority);
        }
    }


    
    public void StartVoice( AudioClip acVoice)
    {
        if (asVoice == null)
        {
            Debug.Log("Live2D : AudioSource Component is NULL !");
            return;
        }
        asVoice.clip = acVoice;
		asVoice.loop = false;
#if UNITY_5
		asVoice.spatialBlend = 0;
#else
		asVoice.panLevel = 0;
#endif
        asVoice.Play();
    }


    
    public void SetExpression(string name)
    {
        if (!expressions.ContainsKey(name)) return;
        if (LAppDefine.DEBUG_LOG) Debug.Log("Setting expression : " + name);
        AMotion motion = expressions[name];
        expressionManager.startMotion(motion, false);
    }


    
    public void SetRandomExpression()
    {
        int no = (int)(rand.NextDouble() * expressions.Count);

        string[] keys = new string[expressions.Count];
        expressions.Keys.CopyTo(keys, 0);

        SetExpression(keys[no]);
    }


    
    public bool HitTest(string id, float testX, float testY)
    {

        if (modelSetting == null) return false;
        int len = modelSetting.GetHitAreasNum();
        for (int i = 0; i < len; i++)
        {
            if (id.Equals(modelSetting.GetHitAreaName(i)))
            {
                string drawID = modelSetting.GetHitAreaID(i);
                return hitTestSimple(drawID,testX,testY);
            }
        }

        return false;
    }
   

    public Live2DModelUnity GetLive2DModelUnity()
    {
        return (Live2DModelUnity)live2DModel;
    }


    public Bounds GetBounds()
    {
        return bounds;
    }

    
    
    public void FlickEvent(float x, float y)
    {
        if (LAppDefine.DEBUG_LOG) Debug.Log("flick x:" + x + " y:" + y);

        if (HitTest(LAppDefine.HIT_AREA_HEAD, x, y))
        {
            if (LAppDefine.DEBUG_LOG) Debug.Log("Flick face");
            StartRandomMotion(LAppDefine.MOTION_GROUP_FLICK_HEAD, LAppDefine.PRIORITY_NORMAL);
        }
    }


    
    public bool TapEvent(float x, float y)
    {
		Debug.Log("Tap event");
        if (LAppDefine.DEBUG_LOG) Debug.Log("tapEvent view x:" + x + " y:" + y);

        if (HitTest(LAppDefine.HIT_AREA_HEAD, x, y))
        {
			Debug.Log("hit head");
            if (LAppDefine.DEBUG_LOG) Debug.Log("Tapped face");
            SetRandomExpression();
        }
        else if (HitTest(LAppDefine.HIT_AREA_BODY, x, y))
        {
			Debug.Log("hit body");
            if (LAppDefine.DEBUG_LOG) Debug.Log("Tapped body");
            StartRandomMotion(LAppDefine.MOTION_GROUP_TAP_BODY, LAppDefine.PRIORITY_NORMAL);
        }
        return true;
    }


    
    public void ShakeEvent()
    {
        if (LAppDefine.DEBUG_LOG) Debug.Log("Shake Event");

        StartRandomMotion(LAppDefine.MOTION_GROUP_SHAKE, LAppDefine.PRIORITY_FORCE);
    }


    internal void TouchesBegan(Vector3 inputPos)
    {
        view.TouchesBegan(inputPos);
    }

    internal void TouchesMoved(Vector3 inputPos)
    {
        view.TouchesMoved(inputPos);
    }

    internal void TouchesEnded(Vector3 inputPos)
    {
        view.TouchesEnded(inputPos);
    }
}
