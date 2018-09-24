// This part of the program gets, computes,and stores values for use in the Main method.




using System;

public class Circle
{


    private double radius;                      // This is the radius of the circle
    private const double PI = 3.14159;          // This is a universal constant pi


    // This constructor accepts the value of the radius
    public Circle(double rad)
    {

        radius = rad;

    }


    // This constructor sets the initial value of the radius
    public Circle()
    {

        radius = 0.0;

    }


    // This mutator method sets a value for the radius
    public void SetRadius(double rad)
    {

        radius = rad;

    }


    // This accessor method returns the value of the radius
    public double Radius
    {
        get
        {
            return radius;
        }

        set
        {
            if (radius < 0)
            {
                radius = 0;
            }
        }

    }


    // This accessor method computes and returns the area of the circle
    public double GetArea()
    {

        double area = PI * radius * radius;     // To get the area of the circle
        return area;

    }


    // This accessor method computes and returns the circumference of the circle
    public double GetCircumference()
    {

        double circumference = 2 * PI * radius; // To get the circumference of the circle
        return circumference;

    }

}