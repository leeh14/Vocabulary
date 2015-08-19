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
using live2d;


public class AccelHelper
{
	private static float acceleration_x = 0 ;
	private static float acceleration_y = 0 ;
	private static float acceleration_z = 0 ;
	private static float dst_acceleration_x = 0 ;
	private static float dst_acceleration_y = 0 ;
	private static float dst_acceleration_z = 0 ;

	private static float last_dst_acceleration_x = 0 ;
	private static float last_dst_acceleration_y = 0 ;
	private static float last_dst_acceleration_z = 0 ;

	private static long lastTimeMSec = -1 ;
	private static float lastMove ;

	private bool	sensorReady;

	private float[] accel = new float[3] ;


	public AccelHelper() 
	{
		
	}


	
	public float GetShake()
	{
		return lastMove;
	}


	
	public void ResetShake()
	{
		lastMove=0;
	}


	
	public void SetCurAccel( Vector3 acceleration )
	{
		dst_acceleration_x = acceleration.x ;
		dst_acceleration_y = acceleration.y ;
		dst_acceleration_z = acceleration.z ;

		
		float move =
			Fabs(dst_acceleration_x-last_dst_acceleration_x) +
			Fabs(dst_acceleration_y-last_dst_acceleration_y) +
			Fabs(dst_acceleration_z-last_dst_acceleration_z) ;
		lastMove = lastMove * 0.7f + move * 0.3f ;

		last_dst_acceleration_x = dst_acceleration_x ;
		last_dst_acceleration_y = dst_acceleration_y ;
		last_dst_acceleration_z = dst_acceleration_z ;
	}


	
	public void Update(){
		const float MAX_ACCEL_D = 0.04f ;
		float dx = dst_acceleration_x - acceleration_x ;
		float dy = dst_acceleration_y - acceleration_y ;
		float dz = dst_acceleration_z - acceleration_z ;

		if( dx >  MAX_ACCEL_D ) dx =  MAX_ACCEL_D ;
		if( dx < -MAX_ACCEL_D ) dx = -MAX_ACCEL_D ;

		if( dy >  MAX_ACCEL_D ) dy =  MAX_ACCEL_D ;
		if( dy < -MAX_ACCEL_D ) dy = -MAX_ACCEL_D ;

		if( dz >  MAX_ACCEL_D ) dz =  MAX_ACCEL_D ;
		if( dz < -MAX_ACCEL_D ) dz = -MAX_ACCEL_D ;

		acceleration_x += dx ;
		acceleration_y += dy ;
		acceleration_z += dz ;

		long time = UtSystem.getUserTimeMSec() ;
		long diff = time - lastTimeMSec ;

		lastTimeMSec = time ;

		float scale = 0.2f * diff * 60 / (1000.0f) ;	
		const float MAX_SCALE_VALUE = 0.5f ;
		if( scale > MAX_SCALE_VALUE ) scale = MAX_SCALE_VALUE ;

		accel[0] = (acceleration_x * scale) + (accel[0] * (1.0f - scale)) ;
		accel[1] = (acceleration_y * scale) + (accel[1] * (1.0f - scale)) ;
		accel[2] = (acceleration_z * scale) + (accel[2] * (1.0f - scale)) ;
	}


	
	private float Fabs(float v)
	{
		return v > 0 ? v : -v ;
	}


	
	public float GetAccelX() {
		return accel[0];
	}


	
	public float GetAccelY() {
		return accel[1];
	}


	
	public float GetAccelZ() 
	{
		return accel[2];
	}
}