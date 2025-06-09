# Iterable 객체
- 반복 가능한 객체로, 배열을 일반화한 객체
  - 어떤 객체든 for..of 반복문 적용
- 이터러블 객체의 핵심은 '관심사의 분리(Separation of concern, SoC)'에 있습니다.

### Symbol.iterator
```js
let range = {
  from: 1,
  to: 5
};

// range를 반복 가능한 객체로 만들어주는 코드
// 1. for..of 최초 호출 시, Symbol.iterator가 호출됩니다.
range[Symbol.iterator] = function() {

  // Symbol.iterator는 이터레이터 객체를 반환합니다.
  // 2. 이후 for..of는 반환된 이터레이터 객체만을 대상으로 동작하는데, 이때 다음 값도 정해집니다.
  return {
    current: this.from,
    last: this.to,

    // 3. for..of 반복문에 의해 반복마다 next()가 호출됩니다.
    next() {
      // 4. next()는 값을 객체 {done:.., value :...}형태로 반환해야 합니다.
      if (this.current <= this.last) {
        return { done: false, value: this.current++ };
      } else {
        return { done: true };
      }
    }
  };
};

// 이제 의도한 대로 동작합니다!
for (let num of range) {
  alert(num); // 1, then 2, 3, 4, 5
}
```
```js
// range 자체를 iterable로 만들기
let range = {
  from: 1,
  to: 5,

  [Symbol.iterator]() {
    this.current = this.from;
    return this;
  },

  next() {
    if (this.current <= this.to) {
      return { done: false, value: this.current++ };
    } else {
      return { done: true };
    }
  }
};

for (let num of range) {
  alert(num); // 1, then 2, 3, 4, 5
}
```
- 문자열
```js
for (let char of "test") {
  // 글자 하나당 한 번 실행됩니다(4회 호출).
  alert( char ); // t, e, s, t가 차례대로 출력됨
}

let str = '𝒳😂';
for (let char of str) {
    alert( char ); // 𝒳와 😂가 차례대로 출력됨
}

// for (let char of str) alert(char); 와 동일한 작업
let str = "Hello";
let iterator = str[Symbol.iterator]();
while (true) {
  let result = iterator.next();
  if (result.done) break;
  alert(result.value); // 글자가 하나씩 출력됩니다.
}
```

### 유사 배열
- 유사 배열(array-like) 은 인덱스와 length 프로퍼티가 있어서 배열처럼 보이는 객체입니다.
```js
let arrayLike = {
  0: "Hello",
  1: "World",
  length: 2
};

// Symbol.iterator가 없으므로 에러 발생
for (let item of arrayLike) {}

let arr = Array.from(arrayLike); // (*)
alert(arr.pop()); // World 
```
```js
let range = {
  from: 1,
  to: 5
};
let arr = Array.from(range);
alert(arr); // 1,2,3,4,5 (배열-문자열 형 변환이 제대로 동작합니다.)
```
```js
// 배열의 매핑
let range = { from: 1, to: 5 };
let arr = Array.from(range, num => num * num);
alert(arr); // 1,4,9,16,25

// 배열 분해
let str = '𝒳😂';
let chars = Array.from(str);
alert(chars[0]); // 𝒳
alert(chars[1]); // 😂
alert(chars.length); // 2

let chars = []; // Array.from 내부에선 아래와 동일한 반복문이 돌아갑니다.
for (let char of str) {
  chars.push(char);
}
alert(chars);

// Slice 직접 구현
function slice(str, start, end) {
  return Array.from(str).slice(start, end).join('');
}
```