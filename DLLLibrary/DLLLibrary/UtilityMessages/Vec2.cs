/*
 * Saxion, Game Architecture
 * User: Eelco Jannink
 * Date: 5/22/2016
 * Time: 12:00 PM
 */
using System;
using System.Diagnostics;


public class Vec2
{
    public static Vec2 zero { get { return new Vec2(0, 0); } }
    public static Vec2 temp = new Vec2();

    private float x = 0;
	private float y = 0;
	
	public Vec2() : this( 0, 0 ) {}
	public Vec2( float x, float y )
	{
		this.x = x;
		this.y = y;
	}
	
	public float X {
		get { return x; }
		set { x = value; }
	}

	public float Y {
		get { return y; }
		set { y = value; }
	}
	
	public Vec2 Add( Vec2 other) 
	{
        Vec2 clone = this.Clone();
		Debug.Assert( other != null ,"is null");
		
		clone.x += other.x;
		clone.y += other.y;
        return clone;
    }
    public float Dot(Vec2 one, Vec2 other)
    {
        Debug.Assert(one != null);
        Debug.Assert(other != null);

        return one.x * other.x + one.y * other.y;
    }

    public Vec2 Subtract(Vec2 other)
    {
        Vec2 clone = this.Clone();
        Debug.Assert(other != null);

        clone.x -= other.x;
        clone.y -= other.y;
        return clone;
    }


    public float Distance( Vec2 one, Vec2 other )
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		Vec2 difference = one - other;
		return (float) Math.Sqrt( difference.x*difference.x + difference.y*difference.y );
	}
	
	public float DistanceTo(Vec2 other )
	{
		Debug.Assert( this != null );
		Debug.Assert( other != null );
		
		Vec2 difference = this - other;
		return (float)Math.Sqrt(difference.x*difference.x + difference.y*difference.y);
    }
    public float Length()
    {
        return (float)Math.Sqrt(x * x + y * y);
    }

    public Vec2 Normalize()
    {
        if (x == 0 && y == 0)
        {
            return this;
        }
        else
        {
            return Scale(1 / Length());
        }
    }

    public Vec2 Clone()
    {
        return new Vec2(x, y);
    }
    public Vec2 Scale(float scalar)
    {
        x *= scalar;
        y *= scalar;
        return this;
    }

    public Vec2 SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
        return this;
    }
    public static float Deg2Rad(float degrees)
    {
        return degrees / 180 * (float)Math.PI;
    }

    public static float Rad2Deg(float radians)
    {
        return radians / (float)Math.PI * 180;
    }

    public static Vec2 GetUnitVectorDegrees(float degrees)
    {
        degrees = Deg2Rad(degrees);
        Vec2 NewVector = new Vec2();
        NewVector.x = (float)Math.Cos(degrees);
        NewVector.y = (float)Math.Sin(degrees);
        return NewVector;
    }

    public static Vec2 GetUnitVectorRadians(float radians)
    {
        Vec2 NewVector = new Vec2();
        NewVector.x = (float)Math.Cos(radians);
        NewVector.y = (float)Math.Sin(radians);
        return NewVector;
    }

    //public static Vec2 RandomUnitVector()
    //{
    //    Vec2 newVector = new Vec2(1, 0);
    //    int angle = Utils.Random(0, 361);
    //    newVector.SetAngleDegrees(angle);
    //    return newVector;
    //}

    public void SetAngleDegrees(float angle)
    {
        angle = Deg2Rad(angle);
        SetAngleRadians(angle);
    }

    public void SetAngleRadians(float radian)
    {
        float howlong = Length();
        x = (float)Math.Cos(radian) * howlong;
        y = (float)Math.Sin(radian) * howlong;
    }

    public float GetAngleRadians()
    {
        return (float)Math.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public void RotateDegrees(float angle)
    {
        angle = angle + GetAngleDegrees();
        SetAngleDegrees(angle);
    }

    public void RotateRadians(float radian)
    {
        radian = radian + GetAngleRadians();
        SetAngleRadians(radian);
    }

    public void RotateAroundDegrees(float X, float Y, float degrees)
    {
        degrees = Deg2Rad(degrees);
        float sn = (float)Math.Sin(degrees);
        float cs = (float)Math.Cos(degrees);

        x -= X;
        y -= Y;

        float newx = x * cs - y * sn;
        float newy = x * sn + y * cs;

        x = newx + X;
        y = newy + Y;
    }

    public void RotateAroundRadians(float X, float Y, float radians)
    {
        float sn = (float)Math.Sin(radians);
        float cs = (float)Math.Cos(radians);

        x -= X;
        y -= Y;

        float newx = x * cs - y * sn;
        float newy = x * sn + y * cs;

        x = newx + X;
        y = newy + Y;
    }

    public void ReflectOnPoint(Vec2 other, Vec2 other1, float elasticity)
    {
        Vec2 _tempVec = other.Clone().Subtract(other1).Normal();
        this.Subtract(_tempVec.Clone().Scale(2 * this.Dot(_tempVec) * elasticity));
    }

    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalize();
    }

    //public void Reflect(Vec2 other,float elasticity)
    //{
    //    this.Subtract(other.Normal().Clone().Scale( 2*this.Dot(other.Normal().Clone())));
    //    this.Scale(elasticity);
    //}

    public void Reflect(Vec2 pOther, float pElasticity)
    {
        this.Subtract(pOther.Normal().Clone().Scale((1 + pElasticity) * this.Dot(pOther.Normal().Clone())));
    }

    public float Dot(Vec2 pOther)
    {
        return x * pOther.x + y * pOther.y;
    }

    public void ReflectOnPoint(Vec2 pNormal, float pElasticity)
    {
        //Vec2 _tempVec = other1.Clone().Subtract(other).Normalize();
        this.Subtract(pNormal.Clone().Scale((1 + pElasticity) * this.Dot(pNormal)));
        //this.Scale(elasticity);
    }

	// operator overloading, be carefull about semantics
	static public Vec2 operator +( Vec2 one, Vec2 other) 
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		return new Vec2( one.x+other.x, one.y+other.y );
	}
	
	static public Vec2 operator -( Vec2 one, Vec2 other) 
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		return new Vec2( one.x-other.x, one.y-other.y );
	}

	static public Vec2 operator *( Vec2 one, float value )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x*value, one.y*value );
	}
	static public Vec2 operator *( float value, Vec2 one )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x*value, one.y*value );
	}
	static public Vec2 operator /( Vec2 one, float value )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x/value, one.y/value );
	}
	static public Vec2 operator /( float value, Vec2 one )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x/value, one.y/value );
	}
	
	override public string ToString() 
	{
		return "( "+x+" , "+y+" )";
	}

}

