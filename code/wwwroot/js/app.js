// Number to Words Converter - Client-side JavaScript

document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('converterForm');
    const numberInput = document.getElementById('numberInput');
    const resultSection = document.getElementById('resultSection');
    const resultDisplay = document.getElementById('resultDisplay');
    const errorSection = document.getElementById('errorSection');
    const errorDisplay = document.getElementById('errorDisplay');
    const convertButton = document.getElementById('convertButton');

    // Handle form submission
    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        await convertNumber();
    });

    // Allow Enter key to submit
    numberInput.addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            convertNumber();
        }
    });

    // Clear error when user starts typing
    numberInput.addEventListener('input', function() {
        if (errorSection.classList.contains('visible')) {
            hideError();
        }
    });

    async function convertNumber() {
        const value = numberInput.value.trim();

        // Basic client-side validation
        if (!value) {
            showError('Please enter a number');
            return;
        }

        // Show loading state
        convertButton.disabled = true;
        convertButton.textContent = 'Converting...';
        hideError();
        hideResult();

        try {
            const response = await fetch('/api/conversion/convert', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ value: value })
            });

            const data = await response.json();

            if (response.ok && data.success) {
                showResult(data.words);
            } else {
                showError(data.error || 'An error occurred during conversion');
            }
        } catch (error) {
            console.error('Conversion error:', error);
            showError('Unable to connect to the server. Please try again.');
        } finally {
            // Reset button state
            convertButton.disabled = false;
            convertButton.textContent = 'Convert to Words';
        }
    }

    function showResult(words) {
        resultDisplay.textContent = words;
        resultSection.classList.remove('hidden');
        resultSection.classList.add('visible');
        
        // Animate the result
        resultDisplay.style.opacity = '0';
        setTimeout(() => {
            resultDisplay.style.opacity = '1';
        }, 100);
    }

    function hideResult() {
        resultSection.classList.remove('visible');
        resultSection.classList.add('hidden');
    }

    function showError(message) {
        errorDisplay.textContent = message;
        errorSection.classList.remove('hidden');
        errorSection.classList.add('visible');
    }

    function hideError() {
        errorSection.classList.remove('visible');
        errorSection.classList.add('hidden');
    }

    // Focus on input field when page loads
    numberInput.focus();
});