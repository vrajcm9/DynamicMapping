# DynamicMapping

## Develop a Dynamic Mapping System Using .NET

### System Architecture

- **Main Program File**
  - Executes the main logic and calls all the defined classes and functions.

- **Project Structure**
  - **Model Folder**
    - Contains required classes for the structure of data.
    - Can be extended with additional model classes as needed.
    - Includes a sample `Reservation` model for testing.
  - **Utilities Folder**
    - Contains the `MapHandler` class, which handles the mapping of a model between JSON and XML.
    - Methods are defined as generics, so any model can be passed.
    - Includes a custom error handler class to manage any errors thrown in the program, providing error messages or status codes as output.

### Testing and Additional Details

- **Endpoints for Conversion Testing**
  - JSON to XML: `http://localhost:5194/reservation/json-to-xml`
  - XML to JSON: `http://localhost:5194/reservation/xml-to-json`

- **Extensive Testing Endpoint**
  - General conversion endpoint: `http://localhost:5194/reservation/convert`
    - Converts JSON to XML: `http://localhost:5194/reservation/convert?sourceType=JSON&targetType=XML`
      - Requires JSON data:
        ```json
        {
            "Reservation": {
                "Id": 123,
                "Name": "John",
                "BookingType": "Room",
                "Amount": 32
            }
        }
        ```
    - Converts XML to JSON: `http://localhost:5194/reservation/convert?sourceType=XML&targetType=JSON`
      - Requires XML data:
        ```xml
        <ReservationCollectionModel>
            <Reservation>
                <Id>123</Id>
                <Name>John</Name>
                <BookingType>Room</BookingType>
                <Amount>32</Amount>
            </Reservation>
        </ReservationCollectionModel>
        ```
    - Tests error handling: `http://localhost:5194/reservation/convert?sourceType=XML&targetType=Test`

- **Testing Tools**
  - Postman is used for testing the endpoints.

### Assumptions and Limitations

- The program works with models instead of directly converting between JSON and XML (e.g., using Newtonsoft.JSON for direct conversion).
- Additional elements or properties not mapped or not available in the model will be ignored.
