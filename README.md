# NumberToWords - Currency Converter Web Application

A web application that converts numerical currency values into their written word representation.

## Features

- Converts numbers to words in USD currency format
- Supports values from -999,999,999.99 to 999,999,999.99
- Clean, responsive web interface
- RESTful API endpoint
- Real-time conversion with error handling

## Example

**Input:** `123.45`  
**Output:** `ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS`

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- A modern web browser (Chrome, Firefox, Safari, or Edge)

## Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/NathanPerrier/NumberToWords.git
cd NumberToWords
```

### 2. Build the Application
```bash
cd code
dotnet restore
dotnet build
```

### 3. Run the Application
```bash
dotnet run --launch-profile http
```

The application will start and be available at:
- Web Interface: http://localhost:5122
- Swagger API Documentation: http://localhost:5122/swagger

### 4. Use the Application

#### Via Web Interface:
1. Open http://localhost:5122 in your browser
2. Enter a number (e.g., 123.45)
3. Click "Convert to Words" or press Enter
4. View the result

#### Via API:
```bash
curl -X POST "http://localhost:5122/api/conversion/convert" \
     -H "Content-Type: application/json" \
     -d '{"value": "123.45"}'
```

## Project Structure

```
NumberToWords/
├── code/                      # Main application code
│   ├── Controllers/           # API controllers
│   ├── Services/              # Business logic services
│   ├── wwwroot/              # Static web files
│   │   ├── index.html        # Main web interface
│   │   ├── css/              # Stylesheets
│   │   └── js/               # JavaScript files
│   ├── Program.cs            # Application entry point
│   └── NumbersToWords.csproj # Project configuration
├── DESIGN_APPROACH.md        # Design decisions document
├── TEST_PLAN.md             # Testing strategy document
└── README.md                # This file
```

## API Documentation

### Convert Number to Words

**Endpoint:** `POST /api/conversion/convert`

**Request Body:**
```json
{
    "value": "123.45"
}
```

**Success Response:**
```json
{
    "success": true,
    "words": "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS",
    "originalValue": "123.45"
}
```

**Error Response:**
```json
{
    "success": false,
    "error": "Invalid format. Please enter a valid number (e.g., 123.45)"
}
```

### Health Check

**Endpoint:** `GET /api/conversion/health`

**Response:**
```json
{
    "status": "healthy",
    "timestamp": "2024-01-20T10:30:00Z"
}
```

## Input Validation Rules

- Numbers must be between -999,999,999.99 and 999,999,999.99
- Maximum of 2 decimal places
- Valid formats: `123`, `123.45`, `-123.45`
- Invalid formats: `abc`, `12.345`, `1,000`

## Development

### Running with HTTPS
```bash
dotnet run --launch-profile https
```
Access at: https://localhost:7124

### Running Tests
Currently, manual testing is performed using the TEST_PLAN.md document. Unit tests can be added by creating a test project:

```bash
dotnet new xunit -n NumbersToWords.Tests
dotnet sln code/NumbersToWords.sln add NumbersToWords.Tests/NumbersToWords.Tests.csproj
```

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers (iOS Safari, Chrome Android)

## Troubleshooting

### Port Already in Use
If you see an error about port 5122 being in use:
1. Change the port in `code/Properties/launchSettings.json`
2. Or find and stop the process using the port

### Build Errors
Ensure you have .NET 8.0 SDK installed:
```bash
dotnet --version
```

### API Not Responding
1. Check that the application is running
2. Verify the correct URL and port
3. Check browser console for JavaScript errors

## Contributing

Please ensure all changes:
1. Follow the existing code style
2. Include appropriate error handling
3. Update documentation as needed
4. Pass all test cases in TEST_PLAN.md

## License

This project is created as part of an engineering assessment.
