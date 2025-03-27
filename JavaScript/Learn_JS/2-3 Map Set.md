# Map, Set
- 복잡한 자료구조
  - 객체 - 키가 있는 컬렉션
  - 배열 - 순서가 있는 컬렉션
  - Map - 키가 있는 컬렉션, 키에 다양한 자료형 허용
  - Set - 순서가 있고 중복 허용하지 않는 컬렉션

# Map
```js
let map = new Map();
map.set('1', 'str1');   // 문자형 키
map.set(1, 'num1');     // 숫자형 키
map.set(true, 'bool1'); // 불린형 키
let john = { name: "John" };
map.set(john, 123); // 객체형 키

alert( map.get(1)   ); // 'num1'
alert( map.get('1') ); // 'str1'
alert( map.get(12) ); // undefined
alert( map.get(john) ); // 123
alert( map.size ); // 3
// map[key] 방식을 올바른 방법이 아닙니다. ([] 사용하는 순간 객체로 취급)

// 객체로 Map 객체형 키 흉내
let john = { name: "John" };
let visitsCountObj = {}; // 객체를 하나 만듭니다.
visitsCountObj[john] = 123; // 객체(john)를 키로 해서 객체에 값(123)을 저장해봅시다.
alert( visitsCountObj["[object Object]"] ); // 123 // 원하는 값을 얻으려면 키가 들어갈 자리에 `object Object`를 써줘야합니다.

// 체이닝
map.set('1', 'str1')
  .set(1, 'num1')
  .set(true, 'bool1');

// Map 초기화
let map = new Map([
  ['1',  'str1'],
  [1,    'num1'],
  [true, 'bool1']
]);
let recipeMap = new Map([
  ['cucumber', 500],
  ['tomatoes', 350],
  ['onion',    50]
]);

for (let vegetable of recipeMap.keys()) { // 키(vegetable)를 대상으로 순회합니다.
  alert(vegetable); // cucumber, tomatoes, onion
}

for (let amount of recipeMap.values()) { // 값(amount)을 대상으로 순회합니다.
  alert(amount); // 500, 350, 50
}

for (let entry of recipeMap) { // recipeMap.entries()와 동일합니다. // [키, 값] 쌍을 대상으로 순회합니다.
  alert(entry); // cucumber,500 ...
}

// foreach
recipeMap.forEach( (value, key, map) => {
  alert(`${key}: ${value}`); // cucumber: 500 ...
});

// 객체 -> 맵 : Object.entries로 객체를 맵으로 변경
let obj = {
  name: "John",
  age: 30
};
let map = new Map(Object.entries(obj));

alert( map.get('name') ); // John

// 맵 -> 객체 : Object.fromEntries
let prices = Object.fromEntries([
  ['banana', 1],
  ['orange', 2],
  ['meat', 4]
]);

// now prices = { banana: 1, orange: 2, meat: 4 }
alert(prices.orange); // 2

let map = new Map();
map.set('banana', 1);
map.set('orange', 2);
map.set('meat', 4);
let obj = Object.fromEntries(map.entries());
let obj = Object.fromEntries(map); // .entries() 생략 가능

// obj = { banana: 1, orange: 2, meat: 4 }
alert(obj.orange); // 2
```

# Set
```js
let set = new Set();

let john = { name: "John" };
let pete = { name: "Pete" };
let mary = { name: "Mary" };

// 어떤 고객(john, mary)은 여러 번 방문할 수 있습니다.
set.add(john);
set.add(pete);
set.add(mary);
set.add(john);
set.add(mary);
alert( set.size ); // 3

for (let user of set) {
  alert(user.name); // // John, Pete, Mary 순으로 출력됩니다.
}

let set = new Set(["oranges", "apples", "bananas"]);
for (let value of set) alert(value);
set.forEach((value, valueAgain, set) => {
  alert(value);
});

```