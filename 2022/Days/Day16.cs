using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Tunnel
    {
        public int rate;
        public string[] connections;

        public Tunnel(int rate, string[] connections)
        {
            this.rate = rate;
            this.connections = connections;
        }
    }

    class Day16 : Day
    {
        string[] lines = File.ReadAllLines("Input/16.txt");
        Dictionary<string, Tunnel> tunnels = new();
        Dictionary<string, ulong> bitMap = new();
        Dictionary<State, int> done = new();

        class State
        {
            public int timeLeft;
            public ulong valvesLeft;
            public string pos;
            public string elephantPos;

            public State(int timeLeft, ulong valvesLeft, string pos, string elephantPos)
            {
                this.timeLeft = timeLeft;
                this.valvesLeft = valvesLeft;
                this.pos = pos;
                this.elephantPos = elephantPos;
            }

            public bool IsValveOpen(ulong b)
            {
                return (valvesLeft & b) > 0;
            }

            public ulong CloseValve(ulong b)
            {
                return valvesLeft & ~b;
            }

            public override bool Equals(object obj)
            {
                return obj is State state &&
                       timeLeft == state.timeLeft &&
                       valvesLeft == state.valvesLeft &&
                       pos == state.pos &&
                       elephantPos == state.elephantPos;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(timeLeft, valvesLeft, pos, elephantPos);
            }
        }

        int stepP(string valve, int timeLeft)
        {
            return timeLeft * tunnels[valve].rate;
        }

        int Attempt(State s, bool elephants)
        {
            if (done.ContainsKey(s))
                return done[s];

            if (s.timeLeft == 0)
                return 0;

            Tunnel tunnel = tunnels[s.pos];
            int pressureAdded = 0;

            ulong bit = bitMap[s.pos];
            if (tunnel.rate > 0 && s.IsValveOpen(bit))
            {
                int newPressure = stepP(s.pos, s.timeLeft - 1);

                if (elephants)
                {
                    int ePressure = stepP(s.elephantPos, s.timeLeft - 1);
                    ulong eBit = bitMap[s.elephantPos];
                    Tunnel eTunnel = tunnels[s.elephantPos];

                    // both close
                    if (eTunnel.rate > 0 && s.IsValveOpen(eBit) && s.pos != s.elephantPos)
                        pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.CloseValve(bit) & s.CloseValve(eBit), s.pos, s.elephantPos), elephants) + newPressure + ePressure);

                    foreach (string c in eTunnel.connections)
                        pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.CloseValve(bit), s.pos, c), elephants) + newPressure);
                }
                else
                    pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.CloseValve(bit), s.pos, s.elephantPos), elephants) + newPressure);
            }

            foreach (string c in tunnel.connections)
            {
                if (elephants)
                {
                    int ePressure = stepP(s.elephantPos, s.timeLeft - 1);
                    ulong eBit = bitMap[s.elephantPos];
                    Tunnel eTunnel = tunnels[s.elephantPos];

                    if (eTunnel.rate > 0 && s.IsValveOpen(eBit))
                        pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.CloseValve(eBit), c, s.elephantPos), elephants) + ePressure);

                    foreach (string cc in eTunnel.connections)
                        pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.valvesLeft, c, cc), elephants));
                }
                else
                    pressureAdded = Math.Max(pressureAdded, Attempt(new State(s.timeLeft - 1, s.valvesLeft, c, s.elephantPos), elephants));
            }

            done.Add(s, pressureAdded);
            return pressureAdded;
        }

        public override object Part1()
        {
            int i = 0;
            foreach (var line in lines)
            {
                var temp = line.Split();
                var value = temp[1];
                var amount = int.Parse(temp[4].Split('=')[1].TrimEnd(';'));
                var connections = temp.Skip(9).Select(x => x.TrimEnd(',')).ToArray();
                bitMap.Add(value, (ulong)Math.Pow(2, i++));

                tunnels.Add(value, new Tunnel(amount, connections));
            }

            State start = new State(30, ulong.MaxValue, "AA", "");
            return Attempt(start, false);
        }

        public override object Part2()
        {
            State start = new State(26, ulong.MaxValue, "AA", "AA");
            return Attempt(start, true);
        }
    }
}
