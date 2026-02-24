// Export Helper Functions for Syncfusion Spreadsheet

/**
 * Trigger file download from base64 content
 * @param {string} fileName - Name of the file to download
 * @param {string} base64Content - Base64 encoded file content
 */
function triggerFileDownload(fileName, base64Content) {
    const link = document.createElement('a');
    link.href = 'data:application/octet-stream;base64,' + base64Content;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

/**
 * Convert CSV to JSON
 * @param {string} csvContent - CSV content string
 * @returns {Array} Array of objects representing rows
 */
function csvToJson(csvContent) {
    const lines = csvContent.split('\n');
    const headers = lines[0].split(',').map(h => h.trim());
    const json = [];

    for (let i = 1; i < lines.length; i++) {
        if (lines[i].trim() === '') continue;

        const obj = {};
        const values = lines[i].split(',').map(v => v.trim());

        headers.forEach((header, index) => {
            obj[header] = values[index] || '';
        });

        json.push(obj);
    }

    return json;
}

/**
 * Convert JSON to CSV
 * @param {Array} jsonData - Array of objects
 * @returns {string} CSV formatted string
 */
function jsonToCsv(jsonData) {
    if (!jsonData || jsonData.length === 0) return '';

    const headers = Object.keys(jsonData[0]);
    const csv = [];

    // Add header row
    csv.push(headers.join(','));

    // Add data rows
    jsonData.forEach(row => {
        const values = headers.map(header => {
            const value = row[header] || '';
            return typeof value === 'string' && value.includes(',')
                ? `"${value}"`
                : value;
        });
        csv.push(values.join(','));
    });

    return csv.join('\n');
}

/**
 * Copy cell content to clipboard
 * @param {string} content - Content to copy
 */
function copyToClipboard(content) {
    navigator.clipboard.writeText(content).then(() => {
        console.log('Content copied to clipboard!');
    }).catch(err => {
        console.error('Failed to copy:', err);
    });
}

/**
 * Print spreadsheet
 */
function printSpreadsheet() {
    window.print();
}

/**
 * Format currency value
 * @param {number} value - Value to format
 * @param {string} currency - Currency code (default: USD)
 * @returns {string} Formatted currency string
 */
function formatCurrency(value, currency = 'USD') {
    return new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: currency
    }).format(value);
}

/**
 * Format date value
 * @param {Date} date - Date to format
 * @param {string} locale - Locale code (default: en-US)
 * @returns {string} Formatted date string
 */
function formatDate(date, locale = 'en-US') {
    return new Intl.DateTimeFormat(locale, {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    }).format(new Date(date));
}

/**
 * Get spreadsheet cell value
 * @param {object} spreadsheetRef - Reference to spreadsheet component
 * @param {string} cellAddress - Cell address (e.g., 'A1')
 * @returns {any} Cell value
 */
function getCellValue(spreadsheetRef, cellAddress) {
    if (spreadsheetRef && spreadsheetRef.getData) {
        return spreadsheetRef.getData(cellAddress);
    }
    return null;
}

/**
 * Set spreadsheet cell value
 * @param {object} spreadsheetRef - Reference to spreadsheet component
 * @param {string} cellAddress - Cell address (e.g., 'A1')
 * @param {any} value - Value to set
 */
function setCellValue(spreadsheetRef, cellAddress, value) {
    if (spreadsheetRef && spreadsheetRef.updateCell) {
        spreadsheetRef.updateCell(cellAddress, value);
    }
}

/**
 * Get cell range as array
 * @param {object} spreadsheetRef - Reference to spreadsheet component
 * @param {string} range - Range (e.g., 'A1:C10')
 * @returns {Array} 2D array of cell values
 */
function getRangeValues(spreadsheetRef, range) {
    if (spreadsheetRef && spreadsheetRef.getData) {
        return spreadsheetRef.getData(range);
    }
    return null;
}

/**
 * Calculate statistics from range
 * @param {Array} values - Array of numeric values
 * @returns {object} Statistics object
 */
function calculateStatistics(values) {
    if (!values || values.length === 0) return null;

    const numValues = values.filter(v => typeof v === 'number').sort((a, b) => a - b);
    const sum = numValues.reduce((a, b) => a + b, 0);
    const avg = sum / numValues.length;
    const min = numValues[0];
    const max = numValues[numValues.length - 1];

    return {
        count: numValues.length,
        sum: sum,
        average: avg,
        min: min,
        max: max,
        range: max - min
    };
}

/**
 * Validate spreadsheet data
 * @param {Array} data - Data to validate
 * @param {Array} rules - Validation rules
 * @returns {Array} Array of validation errors
 */
function validateData(data, rules) {
    const errors = [];

    data.forEach((row, rowIndex) => {
        rules.forEach(rule => {
            if (rule.type === 'required' && !row[rule.field]) {
                errors.push(`Row ${rowIndex + 1}: ${rule.field} is required`);
            }
            if (rule.type === 'number' && isNaN(row[rule.field])) {
                errors.push(`Row ${rowIndex + 1}: ${rule.field} must be a number`);
            }
            if (rule.type === 'email' && !isValidEmail(row[rule.field])) {
                errors.push(`Row ${rowIndex + 1}: ${rule.field} must be a valid email`);
            }
            if (rule.type === 'minLength' && row[rule.field].length < rule.min) {
                errors.push(`Row ${rowIndex + 1}: ${rule.field} must be at least ${rule.min} characters`);
            }
            if (rule.type === 'maxLength' && row[rule.field].length > rule.max) {
                errors.push(`Row ${rowIndex + 1}: ${rule.field} must be at most ${rule.max} characters`);
            }
        });
    });

    return errors;
}

/**
 * Validate email address
 * @param {string} email - Email to validate
 * @returns {boolean} True if valid email
 */
function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

/**
 * Parse spreadsheet data to JSON
 * @param {Array} data - Spreadsheet data
 * @param {Array} headers - Column headers
 * @returns {Array} Array of JSON objects
 */
function parseSpreadsheetToJson(data, headers) {
    return data.map(row => {
        const obj = {};
        headers.forEach((header, index) => {
            obj[header] = row[index] || '';
        });
        return obj;
    });
}

/**
 * Apply formula to range
 * @param {string} formula - Formula template (e.g., '{0}*2')
 * @param {Array} range - Range of values
 * @returns {Array} Calculated values
 */
function applyFormulaToRange(formula, range) {
    return range.map(value => {
        try {
            return eval(formula.replace('{0}', value));
        } catch (e) {
            console.error('Formula error:', e);
            return null;
        }
    });
}
