/**
 *
 *  You can modify and use this source freely
 *  only for the development of application related Live2D.
 *
 *  (c) Live2D Inc. All rights reserved.
 */
using UnityEngine;
using System;
using System.Collections;




public class TouchManager 
{
	private float startY ;
	private float startX ;

	private float lastX=0 ;
	private float lastY=0 ;
	private float lastX1=0 ;
	private float lastY1=0 ;
	private float lastX2=0 ;
	private float lastY2=0 ;

	private float lastTouchDistance = -1 ;

	private float moveX;
	private float moveY;

	private float scale;

	private bool touchSingle ;
	private bool flipAvailable ;

	
	public void TouchBegan(float x1, float y1, float x2, float y2)
	{
		float dist=Distance( x1,  y1,  x2,  y2);
		float centerX = (lastX1 + lastX2) * 0.5f ;
		float centerY = (-lastY1 -lastY2) * 0.5f ;

		lastX = centerX ;
		lastY = centerY ;
		startX=centerX;
		startY=centerY;
		lastTouchDistance = dist ;
		flipAvailable = true ;
		touchSingle = false ;
	}


	
	public void TouchBegan(float x, float y)
	{
		lastX = x ;
		lastY = -y ;
		startX=x;
		startY=-y;
		lastTouchDistance = -1 ;
		flipAvailable = true ;
		touchSingle = true;
	}


	
	public void TouchesMoved(float x, float y)
	{
		lastX = x ;
		lastY = -y ;
		lastTouchDistance = -1 ;
		touchSingle =true;
	}


	
	public void TouchesMoved(float x1, float y1, float x2, float y2)
	{
		float dist = Distance(x1, y1, x2, y2);
		float centerX = (x1 + x2) * 0.5f ;
		float centerY = (-y1 + -y2) * 0.5f ;

		if( lastTouchDistance > 0 )
		{
			scale = (float) Math.Pow( dist / lastTouchDistance , 0.75 ) ;
			moveX = CalcShift( x1 - lastX1 , x2 - lastX2 ) ;
			moveY = CalcShift( -y1 - lastY1 , -y2 - lastY2 ) ;
		}
		else
		{
			scale =1;
			moveX=0;
			moveY=0;
		}

		lastX = centerX ;
		lastY = centerY ;
		lastX1 = x1 ;
		lastY1 = -y1 ;
		lastX2 = x2 ;
		lastY2 = -y2 ;
		lastTouchDistance = dist ;
		touchSingle =false;
	}


	public float GetCenterX()
	{
		return lastX ;
	}


	public float GetCenterY()
	{
		return lastY ;
	}


	public float GetDeltaX()
	{
		return moveX;
	}


	public float GetDeltaY()
	{
		return moveY;
	}


	public float GetStartX()
	{
		return startX;
	}


	public float GetStartY()
	{
		return startY;
	}


	public float GetScale()
	{
		return scale;
	}


	public float GetX()
	{
		return lastX;
	}


	public float GetY()
	{
		return lastY;
	}


	public float GetX1()
	{
		return lastX1;
	}


	public float GetY1()
	{
		return lastY1;
	}


	public float GetX2()
	{
		return lastX2;
	}


	public float GetY2()
	{
		return lastY2;
	}


	
	private float Distance(float x1, float y1, float x2, float y2)
	{
		return (float) Math.Sqrt( (x1 - x2)*(x1 - x2) + (y1 - y2)*(y1 - y2) ) ;
	}


	
	private float CalcShift( float v1 , float v2 )
	{
		if( (v1>0) != (v2>0) ) return 0 ;

		float fugou = v1 > 0 ? 1 : -1 ;
		float a1 = Math.Abs( v1 ) ;
		float a2 = Math.Abs( v2 ) ;
		return fugou * ( ( a1 < a2 ) ? a1 : a2 ) ;
	}


	
	public float GetFlickDistance()
	{
		return Distance(startX, startY, lastX, lastY);
	}


	public bool IsSingleTouch()
	{
		return touchSingle;
	}


	public bool IsFlickAvailable()
	{
		return flipAvailable;
	}


	public void DisableFlick()
	{
		flipAvailable=false;
	}
}