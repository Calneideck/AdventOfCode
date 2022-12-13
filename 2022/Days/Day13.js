const fs = require('fs')

const lines = fs.readFileSync('../Input/13.txt')
  .toString()
  .split('\n\n')
  .map(x => x.split('\n').map(JSON.parse))

let count = 0

function compareArrays(a, b) {
  for (let i = 0; i < a.length; i++) {
    if (a[i] === undefined) return -1
    if (b[i] === undefined) return 1

    let result
    if (typeof a[i] === 'number' && typeof b[i] === 'number') {
      result = a[i] - b[i]
    } else if (typeof a[i] === 'number') {
      result = compareArrays([a[i]], b[i])
    } else if (typeof b[i] === 'number') {
      result = compareArrays(a[i], [b[i]])
    } else {
      result = compareArrays(a[i], b[i])
    }

    if (result !== 0) return result
  }

  return a.length - b.length
}

let i = 0
for (const [a, b] of lines) {
  i++
  if (compareArrays(a, b) < 0) {
    count += i
  }
}

console.log('part 1:', count)

const packets = lines.flat()
packets.push([[2]])
packets.push([[6]])

let key = 1
packets
  .sort(compareArrays)
  .forEach((p, i) => {
    const j = JSON.stringify(p)
    if (j === '[[2]]' || j === '[[6]]') {
      key *= (i + 1)
    }
  })

console.log('part 2:', key)
