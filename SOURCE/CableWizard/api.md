# API

## GET api/v2/products

```json
[
  {
    "id": 1,
    "name": "cable-name",
    "producer": "Balluff"
  }
]
```

## GET, POST, DELETE api/v2/product-details?id=1

```json
{
  "id": 1,
  "name": "cable-name",
  "producer": "Balluff",
  "wires": [
    "C1",
    "C2",
    "C3"
  ],
  "connectors": [
    {
      "type": "M12A3PinFemale",
      "pins": [
        {
          "type": "PinType",
          "name": "1",
          "connectedWire": "C1"
        }
      ]
    }
  ]
}
```

## GET api/v2/caex-document?id=1&version=CAEX2_15

file.aml as blob or text?

