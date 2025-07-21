# Test Plan - Number to Words Converter

## 1. Overview
This test plan describes the testing approach for the Number to Words Converter web application, which converts numerical currency values into their written word representation.

## 2. Test Scope

### In Scope:
- API endpoint functionality
- Input validation
- Number to words conversion accuracy
- Web interface functionality
- Error handling and messaging
- Performance under normal load
- Cross-browser compatibility

### Out of Scope:
- Load testing beyond 100 concurrent users
- Security penetration testing
- Internationalization/localization

## 3. Test Cases

### 3.1 API Endpoint Tests

#### TC001: Valid Number Conversion
- **Endpoint**: POST /api/conversion/convert
- **Test Data**:
  - 123.45 → "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS"
  - 1000 → "ONE THOUSAND DOLLARS"
  - 0 → "ZERO DOLLARS"
  - 0.01 → "ZERO DOLLARS AND ONE CENT"
  - 0.99 → "ZERO DOLLARS AND NINETY-NINE CENTS"
  - 999999999.99 → "NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS"

#### TC002: Negative Number Handling
- **Test Data**:
  - -50 → "NEGATIVE FIFTY DOLLARS"
  - -123.45 → "NEGATIVE ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS"
  - -0.50 → "NEGATIVE ZERO DOLLARS AND FIFTY CENTS"

#### TC003: Boundary Values
- **Test Data**:
  - 999999999.99 (maximum)
  - -999999999.99 (minimum)
  - 0.00 (zero)
  - 0.01 (smallest positive)
  - -0.01 (smallest negative)

#### TC004: Invalid Input Handling
- **Test Data**:
  - Empty string → Error: "Please provide a valid number"
  - "abc" → Error: "Invalid format"
  - "123.456" → Error: "Invalid format" (more than 2 decimal places)
  - "1000000000" → Error: "Number must be between -999,999,999.99 and 999,999,999.99"
  - "12.3.4" → Error: "Invalid format"
  - Special characters (!@#$%) → Error: "Invalid format"

### 3.2 Web Interface Tests

#### TC005: Form Submission
- Enter valid number and click "Convert to Words"
- Enter valid number and press Enter key
- Verify loading state during conversion
- Verify result display

#### TC006: Error Display
- Submit empty form
- Enter invalid characters
- Verify error messages appear and disappear when corrected

#### TC007: User Experience
- Auto-focus on input field on page load
- Clear error message when user starts typing
- Responsive design on mobile/tablet/desktop

### 3.3 Integration Tests

#### TC008: End-to-End Conversion Flow
1. Load web page
2. Enter "250.75" in input field
3. Click "Convert to Words"
4. Verify API call is made
5. Verify result displays: "TWO HUNDRED AND FIFTY DOLLARS AND SEVENTY-FIVE CENTS"

#### TC009: Multiple Sequential Conversions
- Perform 5 conversions in succession
- Verify each result displays correctly
- Verify no memory leaks or performance degradation

### 3.4 Browser Compatibility Tests

#### TC010: Cross-Browser Testing
Test on:
- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile Safari (iOS)
- Chrome Mobile (Android)

## 4. Test Data Sets

### Positive Test Data:
- 1, 10, 19, 20, 99, 100, 101, 119, 120, 999
- 1000, 1001, 10000, 100000, 1000000
- 0.01, 0.10, 0.25, 0.50, 0.75, 0.99
- 12.34, 123.45, 1234.56, 12345.67

### Negative Test Data:
- -1, -99, -100, -999, -1000
- -0.01, -0.99, -123.45

### Edge Cases:
- Leading zeros: 00123.45
- Trailing zeros: 123.00
- Multiple decimal points: 12.34.56
- Scientific notation: 1.23e5
- Very long numbers: 99999999999999

## 5. Test Environment

### Development Environment:
- .NET 8.0 Runtime
- Windows/Mac/Linux development machines
- Local IIS Express or Kestrel

### Test Browsers:
- Latest stable versions of Chrome, Firefox, Safari, Edge
- Mobile emulators for iOS/Android testing

## 6. Test Execution

### Manual Testing Steps:
1. Build and run the application locally
2. Execute all test cases from Section 3
3. Document results in test execution report
4. Log any defects found

### Automated Testing (Future Enhancement):
- Unit tests for NumberToWordsService
- API integration tests
- UI automation with Selenium/Playwright

## 7. Pass/Fail Criteria

### Pass Criteria:
- All valid inputs produce correct word representation
- All invalid inputs produce appropriate error messages
- UI is responsive and functions correctly
- No console errors in browser
- Response time < 1 second for conversion

### Fail Criteria:
- Incorrect conversion results
- Application crashes or hangs
- Missing error handling
- UI elements not functioning
- Response time > 3 seconds

## 8. Test Deliverables

1. Test Plan (this document)
2. Test Execution Report
3. Defect Log (if any)
4. Test Data Files
5. Screenshots of test results

## 9. Risk Assessment

### High Risk Areas:
- Large number conversions (near boundaries)
- Decimal precision handling
- Concurrent user requests

### Mitigation:
- Thorough boundary testing
- Input validation testing
- Basic load testing with multiple browser tabs