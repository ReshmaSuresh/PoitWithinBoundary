using System;

namespace Zoom
{
    class Boundary{

    class Point{
        public float x;
        public float y;

        public Point(float x, float y){
            this.x = x;
            this.y = y;
        }
    }

    static bool isOnLine(Point p1, Point pointToCheck,Point p2){
        if(pointToCheck.x <= Math.Max(p1.x, p2.x) && pointToCheck.x >= Math.Min(p1.x, p2.x) && pointToCheck.y <= Math.Max(p1.y, p2.y) && pointToCheck.y >= Math.Min(p1.y, p2.y)){
            return true;
        }
        return false;
    }

    static int getOrientation(Point p1, Point p2, Point pointFromTheOthersegment){
        float difference = ((p2.y - p1.y) * (pointFromTheOthersegment.x - p2.x)) - ((pointFromTheOthersegment.y-p2.y) * (p2.x - p1.x));

        if(difference == 0){
            return 0; //collinear
        }
        else if (difference  > 0){
            return 1; // clockwise
        }
        else{
            return 2; // anticlockwise
        }
    }

    static bool linesIntersect(Point p1, Point p2, Point q1, Point q2){
        int orietation1 = getOrientation(p1, p2, q1);
        int orietation2 = getOrientation(p1, p2, q2);
        int orietation3 = getOrientation(q1, q2, p1);
        int orietation4 = getOrientation(q1, q2, p2);

        if(orietation1 != orietation2 && orietation3 != orietation4){
            return true;
        }

        if(orietation1 == 0 && isOnLine(p1, p2, q1)){
            return true;
        }

        if(orietation2 == 0 && isOnLine(p1, p2, q2)){
            return true;
        }

        if(orietation3 == 0 && isOnLine(q1, q2, p1)){
            return true;
        }

        if(orietation4 == 0 && isOnLine(q1, q2, p2)){
            return true;
        }
        
        return false;
    }

    static bool isPointInsideBoundary(Point[] boundary, Point pointToCheck){
        Point extremePoint = new Point(1000, pointToCheck.y);
        int count = 0, i = 0; 
        bool passesThroughPoint = false;

        do
        { 
            int next = (i + 1) % boundary.Length; 
  
            if (linesIntersect(boundary[i], boundary[next], pointToCheck, extremePoint))  
            { 
                if (getOrientation(boundary[i], pointToCheck, boundary[next]) == 0) 
                { 
                    return isOnLine(boundary[i], pointToCheck, boundary[next]); 
                } 

                if(getOrientation(pointToCheck, extremePoint, boundary[next]) == 0){
                    next = next + 1;
                    passesThroughPoint = true;
                }
                else{
                    count++; 
                }
                
            } 
            i = next; 
        } while (i != 0); 
        if(passesThroughPoint == true){
            count++;
        }
        if(count % 2 == 0){
            return false;
        }
        else{
            return true;
        }

    }

    public static void  Main(string[] args){

        Point [] boundary = {new Point(-1, 3),
                            new Point(1.5f, 5),
                            new Point(3.5f, 4),
                            new Point(4.8f, 2),
                            new Point(4.8f, -3),
                            new Point(3, -4),
                            new Point(3, -1),
                            new Point(0.5f, -1),
                            new Point(0.5f, -2),
                            new Point(-2, 1)
                            };

        Point pointToCheck = new Point(3.5f, 1);

        Console.WriteLine(isPointInsideBoundary(boundary, pointToCheck));



                                    
        

    }

}
}
