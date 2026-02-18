// SPARQL Examples Page JavaScript
// Handles interactive query display, copying, and syntax highlighting

let allExpanded = false;
let mouseDownPos = null;

// Standard prefixes to prepend when copying
const STANDARD_PREFIXES = `PREFIX nlo: <http://nulllogicone.net/schema.rdfs#>
PREFIX entity: <http://nulllogicone.net/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>

`;

async function loadQueries() {
    const container = document.getElementById('queries-container');
    
    try {
        console.log('Fetching queries...');
        const response = await fetch('/example-queries.sparql');
        if (!response.ok) throw new Error('Failed to load queries');
        
        console.log('Parsing queries...');
        const content = await response.text();
        const queries = parseQueries(content);
        console.log('Found', queries.length, 'queries');
        
        if (queries.length === 0) {
            container.innerHTML = '<div class="error">No queries found in the file.</div>';
            return;
        }
        
        console.log('Rendering queries...');
        container.innerHTML = queries.map((query, index) => `
            <div class="query-section" id="query-${index}">
                <div class="query-header" onclick="toggleQuery(${index})">
                    <h3>${query.title}</h3>
                    <span class="toggle-icon" id="icon-${index}">▼</span>
                </div>
                <div class="query-body" id="body-${index}">
                    <div class="query-code" id="code-${index}" onmousedown="handleQueryMouseDown(event, ${index})" onmouseup="handleQueryMouseUp(event, ${index})" title="Click to copy query with prefixes" style="cursor: text;">${highlightSPARQL(query.code)}</div>
                </div>
            </div>
        `).join('');
        
    } catch (error) {
        container.innerHTML = `
            <div class="error">
                <strong>Error loading queries:</strong><br>
                ${error.message}
            </div>
        `;
    }
}

function parseQueries(content) {
    const queries = [];
    const lines = content.split('\n');
    let i = 0;
    
    // Skip header lines until we find first separator
    while (i < lines.length && !lines[i].includes('========')) {
        i++;
    }
    
    while (i < lines.length) {
        // Look for separator line
        if (lines[i].includes('========')) {
            i++; // Move to next line after separator
            
            // Next line should be the title
            if (i < lines.length && lines[i].trim().startsWith('#')) {
                const title = lines[i].replace(/^#\s*\d*\.?\s*/, '').trim();
                i++; // Move past title
                
                // Skip the closing separator line
                if (i < lines.length && lines[i].includes('========')) {
                    i++;
                }
                
                // Collect code lines until next separator or end
                const codeLines = [];
                while (i < lines.length && !lines[i].includes('========')) {
                    codeLines.push(lines[i]);
                    i++;
                }
                
                let code = codeLines.join('\n').trim();
                
                // Remove PREFIX declarations for display (they'll be added back on copy)
                code = code.split('\n')
                    .filter(line => !line.trim().toUpperCase().startsWith('PREFIX'))
                    .join('\n')
                    .trim();
                
                if (code && title) {
                    queries.push({ title, code });
                }
            }
        } else {
            i++;
        }
    }
    
    return queries;
}

function highlightSPARQL(code) {
    // Basic SPARQL syntax highlighting
    let highlighted = code
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
    
    // Highlight keywords
    const keywords = ['SELECT', 'WHERE', 'PREFIX', 'FILTER', 'OPTIONAL', 'LIMIT', 'ORDER BY', 'GROUP BY', 'COUNT', 'AS', 'CONSTRUCT', 'ASK', 'DESCRIBE', 'FROM', 'UNION', 'DISTINCT', 'REDUCED'];
    keywords.forEach(keyword => {
        const regex = new RegExp(`\\b${keyword}\\b`, 'gi');
        highlighted = highlighted.replace(regex, `<span class="sparql-keyword">${keyword.toUpperCase()}</span>`);
    });
    
    // Highlight variables
    highlighted = highlighted.replace(/(\?\w+)/g, '<span class="sparql-variable">$1</span>');
    
    // Highlight URIs
    highlighted = highlighted.replace(/(&lt;[^&]+&gt;)/g, '<span class="sparql-uri">$1</span>');
    
    // Highlight prefixes (nlo:, rdf:, etc.)
    highlighted = highlighted.replace(/(\w+:)/g, '<span class="sparql-prefix">$1</span>');
    
    // Highlight comments
    highlighted = highlighted.replace(/(#[^\n]*)/g, '<span class="sparql-comment">$1</span>');
    
    return highlighted;
}

function toggleQuery(index) {
    const body = document.getElementById(`body-${index}`);
    const icon = document.getElementById(`icon-${index}`);
    
    body.classList.toggle('expanded');
    icon.classList.toggle('rotated');
}

function toggleAllQueries() {
    const bodies = document.querySelectorAll('.query-body');
    const icons = document.querySelectorAll('.toggle-icon');
    const button = document.querySelector('.expand-all');
    
    allExpanded = !allExpanded;
    
    bodies.forEach(body => {
        if (allExpanded) {
            body.classList.add('expanded');
        } else {
            body.classList.remove('expanded');
        }
    });
    
    icons.forEach(icon => {
        if (allExpanded) {
            icon.classList.add('rotated');
        } else {
            icon.classList.remove('rotated');
        }
    });
    
    button.textContent = allExpanded ? 'Collapse All Queries' : 'Expand All Queries';
}

function handleQueryMouseDown(event, index) {
    // Record mouse position on mouse down
    mouseDownPos = { x: event.clientX, y: event.clientY };
}

async function handleQueryMouseUp(event, index) {
    // Check if mouse moved significantly (user was selecting text)
    if (mouseDownPos) {
        const moved = Math.abs(event.clientX - mouseDownPos.x) > 5 || 
                     Math.abs(event.clientY - mouseDownPos.y) > 5;
        mouseDownPos = null;
        
        // If mouse moved, user was selecting text, don't copy
        if (moved) {
            return;
        }
        
        // Check if there's a text selection
        const selection = window.getSelection();
        if (selection && selection.toString().length > 0) {
            return;
        }
    }
    
    // Simple click detected - copy the query
    await copyQueryWithToast(index);
}

async function copyQueryWithToast(index) {
    const codeElement = document.getElementById(`code-${index}`);
    
    // Add flash animation
    codeElement.classList.add('flash-copy');
    setTimeout(() => {
        codeElement.classList.remove('flash-copy');
    }, 600);
    
    // Get the plain text (without HTML formatting)
    const queryText = codeElement.innerText;
    
    // Prepend standard prefixes to the query
    const textToCopy = STANDARD_PREFIXES + queryText;
    
    try {
        await navigator.clipboard.writeText(textToCopy);
        showToast('✓ Copied to clipboard with prefixes!');
    } catch (err) {
        showToast('✗ Failed to copy to clipboard', true);
    }
}

function showToast(message, isError = false) {
    // Remove any existing toast
    const existingToast = document.querySelector('.toast');
    if (existingToast) {
        existingToast.remove();
    }
    
    // Create toast element
    const toast = document.createElement('div');
    toast.className = 'toast' + (isError ? ' toast-error' : '');
    toast.textContent = message;
    document.body.appendChild(toast);
    
    // Trigger animation
    setTimeout(() => {
        toast.classList.add('show');
    }, 10);
    
    // Remove after animation
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => {
            toast.remove();
        }, 300);
    }, 2000);
}

// Load queries when page loads
document.addEventListener('DOMContentLoaded', loadQueries);
