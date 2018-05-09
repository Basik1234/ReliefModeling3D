﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ReliefModeling.Model.Carriages;
using ReliefModeling.Model.Recognize.Layer;
using ReliefModeling.Services;

namespace ReliefModeling.Model.Recognize
{
    public class FinderEdgeDots : IFinderIsolines
    {
        private RecognizeMap _recognizeMap;
        private Carriage _carriage;
        private Direction _lastDirection;
        private Point _startPoint;
        private MapLegend _mapLegend;
        private IFinderLayer _finderLayer;
        
        public FinderEdgeDots(Bitmap bitmap, Bitmap legend)
        {
            _carriage = default(Carriage);
            _lastDirection = default(Direction);
            _startPoint = default(Point);
            _mapLegend = new MapLegend(legend);
            
            _recognizeMap = bitmap.ConvertColorBitmapInRecognizeMap(_mapLegend);
            _finderLayer = new SpiralFinderLayer(_recognizeMap);
        }
        
        public List<Isoline> Find()
        {
            var isolines = new List<Isoline>();
            var levelLayer = 0;
            
            for (var y = 0; y < _recognizeMap.Height; y++)
            {
                for (var x = 0; x < _recognizeMap.Width; x++)
                {
                    
                    if (!_recognizeMap[x,y].Equals(Const.ID_ISOLINE_MAP)) continue;

                    if (_recognizeMap.GetAroundPixels(x, y).Contains(Const.ID_DISCOVER_ISOLINE_MAP))
                    {
                        _recognizeMap[x, y] = Const.ID_DISCOVER_ISOLINE_MAP;
                        continue;
                    }

                    var isoline = new Isoline();
                    isolines.Add(isoline);
                    
                    _startPoint = new Point(x, y);
                    _carriage = new Carriage(new Point(_startPoint.X-1,_startPoint.Y-1));

                    while (NextStep())
                    {
                        isoline.Dots.AddRange(GetDots());
                    }
                    
                    _finderLayer.FindLevelLayer(isoline);
                }
            }

            return isolines;
        }

        private bool NextStep()
        {
            var dots = InitDotArray();

            Direction direction;
            switch (MaskDots.GetMask(dots))
            {       
                case MaskDots.Masks.DiagonalLeft:
                {
                    switch (_lastDirection)
                    {
                        case Direction.Left:
                            direction = Direction.Up;
                            break;
                        case Direction.Down:
                            direction = Direction.Right;
                            break;
                        case Direction.Right:
                            direction = Direction.Down;
                            break;
                        case Direction.Up:
                            direction = Direction.Left;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }break;
                case MaskDots.Masks.DiagonalRight:
                {
                    switch (_lastDirection)
                    {
                        case Direction.Left:
                            direction = Direction.Down;
                            break;
                        case Direction.Down:
                            direction = Direction.Left;
                            break;
                        case Direction.Right:
                            direction = Direction.Up;
                            break;
                        case Direction.Up:
                            direction = Direction.Right;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }break;
                    
                case MaskDots.Masks.AngleUpRight:
                case MaskDots.Masks.DotDownLeft:
                {
                    direction = _lastDirection == Direction.Right ? Direction.Down : Direction.Left;
                }break;
                    
                case MaskDots.Masks.AngleUpLeft:
                case MaskDots.Masks.DotDownRight:
                {
                    direction = _lastDirection == Direction.Left ? Direction.Down : Direction.Right;
                }break;
                    
                case MaskDots.Masks.Floor:
                case MaskDots.Masks.Ceiling:
                {
                    direction = _lastDirection == Direction.Left ? Direction.Left : Direction.Right;
                }break;

                case MaskDots.Masks.WallRight:
                case MaskDots.Masks.WallLeft:
                {
                    direction = _lastDirection == Direction.Up ? Direction.Up : Direction.Down;
                }break;

                case MaskDots.Masks.AngleDownLeft:
                case MaskDots.Masks.DotUpRight:
                {
                    direction = _lastDirection == Direction.Left ? Direction.Up : Direction.Right;
                }break;

                case MaskDots.Masks.AngleDownRight:
                case MaskDots.Masks.DotUpLeft:
                {
                    direction = _lastDirection == Direction.Right ? Direction.Up : Direction.Left;
                }break;
                    
                case MaskDots.Masks.Full:
                case MaskDots.Masks.Empty:
                {
                    return false;
                }
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _carriage.Move(direction);
            _lastDirection = direction;
            
            return !_carriage.Points[1, 1].Equals(_startPoint);
        }

        private IEnumerable<Point> GetDots()
        {
            var dots = new List<Point>();
            for (var x = 0; x < _carriage.Points.GetLength(0); x++)
            {
                for (var y = 0; y < _carriage.Points.GetLength(1);y++)
                {
                    if(!_recognizeMap.GetPixelSafe(_carriage.Points[x,y]).Equals(Const.ID_ISOLINE_MAP))continue;
                    
                    dots.Add(new Point(_carriage.Points[x,y].X,_carriage.Points[x,y].Y));
                    _recognizeMap[_carriage.Points[x, y].X, _carriage.Points[x, y].Y] = Const.ID_DISCOVER_ISOLINE_MAP;
                }
            }

            return dots;
        }

        private bool[,] InitDotArray()
        {   
            return new [,]{
                {
                    _recognizeMap.GetPixelSafe(_carriage.Points[0, 0]).Equals(Const.ID_ISOLINE_MAP) ||
                    _recognizeMap.GetPixelSafe(_carriage.Points[0, 0]).Equals(Const.ID_DISCOVER_ISOLINE_MAP),
                    _recognizeMap.GetPixelSafe(_carriage.Points[0, 1]).Equals(Const.ID_ISOLINE_MAP) ||
                    _recognizeMap.GetPixelSafe(_carriage.Points[0, 1]).Equals(Const.ID_DISCOVER_ISOLINE_MAP)
                },
                {
                    _recognizeMap.GetPixelSafe(_carriage.Points[1, 0]).Equals(Const.ID_ISOLINE_MAP) ||
                    _recognizeMap.GetPixelSafe(_carriage.Points[1, 0]).Equals(Const.ID_DISCOVER_ISOLINE_MAP),
                    _recognizeMap.GetPixelSafe(_carriage.Points[1, 1]).Equals(Const.ID_ISOLINE_MAP) ||
                    _recognizeMap.GetPixelSafe(_carriage.Points[1, 1]).Equals(Const.ID_DISCOVER_ISOLINE_MAP)
                }
            };
        }

    }
}