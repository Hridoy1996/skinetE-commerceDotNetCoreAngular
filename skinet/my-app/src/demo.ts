let data: number | string;
data = 'a'
data = 1

interface ICar{
    color: string;
    model: string;
    topSpreed?: string;
}
const car1 : ICar = {
    color : 'blue',
    model : 'bmw',
};

const car2 : ICar = {
    color : 'red',
    model : 'toyota',
    topSpreed : '100kmh'
};
const multipy = (x: number, y: number):string => {
  return  (x+y).toString();
};